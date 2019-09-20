using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovingObject
{
    void FixedUpdate()
    {
        PlayerDirection();
        MainController();
    }

    public Vector3 GetPosition(){
        return (transform.position);
    }

    private void PlayerDirection()
    {
        changePos = Vector3.zero;
        changePos.x = Input.GetAxisRaw("Horizontal");
        changePos.y = Input.GetAxisRaw("Vertical");
    }

    private void MainController()
    {
        if (Input.GetButtonDown("Attack") && currentState != ObjectState.attack)
            StartCoroutine(MainAttack());
        else if (changePos != Vector3.zero && currentState != ObjectState.attack)
        {
            SmoothTransition();
            AnimationMovement();
        }
        else
            AnimationIdle();
    }

    private IEnumerator MainAttack()
    {
        DiagonalAttack();
        anime.SetBool("Attacking", true);
        currentState = ObjectState.attack;
        yield return null;

        anime.SetBool("Attacking", false);
        yield return new WaitForSeconds(.44f);

        currentState = ObjectState.walk;
    }

    private void DiagonalAttack()
    {
        if (changePos.x == 0 || changePos.y == 0)
            return;

        float animDirectionX = 0f;
        float animDirectionY = (changePos.y > 0) ? 0f : -1f;

        if (changePos.y > 0)
            animDirectionX = (changePos.x < 0) ? -1f : 1f;

        anime.SetFloat("DirectionX", animDirectionX);
        anime.SetFloat("DirectionY", animDirectionY);
    }

    /*private void DamageFlash()
    {

    }*/

    public void DamageMana(Vector3 knockBackDir, float knockBackDistance, int damage)
    {
        transform.position += knockBackDir * knockBackDistance;
        ManaUI.manaSystemStatic.ChangeMana(damage * -1);
    }

    public void Heal(int heal){
        HeartsHealthUI.heartsHealthSystemStatic.Heal(heal);
    }
    
    public void HealMana(int heal){
        ManaUI.manaSystemStatic.ChangeMana(heal);
    }
}
