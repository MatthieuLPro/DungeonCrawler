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

    private void PlayerDirection()
    {
        changePos = Vector3.zero;
        changePos.x = Input.GetAxisRaw("Horizontal");
        changePos.y = Input.GetAxisRaw("Vertical");
    }

    public override void MainController()
    {
        if ((Input.GetButtonDown("Attack") || Input.GetButtonDown("Carry")) &&
             currentState == ObjectState.carry)
            StartCoroutine(ThrowObject());
        if (Input.GetButtonDown("Attack") && currentState != ObjectState.attack)
            StartCoroutine(MainAttack());
        else if (changePos != Vector3.zero && currentState != ObjectState.attack)
            MoveObject();
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

        currentState = ObjectState.idle;
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

    public void CarryObject(GameObject myObject)
    {
        anime.SetBool("Moving", false);
        anime.SetBool("Carrying", true);
        currentState = ObjectState.carry;

        myObject.GetComponent<CarryObject>().GetTheObject(this.gameObject);
    }

    private IEnumerator ThrowObject()
    {
        currentState = ObjectState.idle;
        yield return new WaitForSeconds(0.5f);
    }
}