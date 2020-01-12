using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovingObject
{
    public bool isWalking;

    void FixedUpdate()
    {
        PlayerDirection();
        MainController();
    }

    private void PlayerDirection()
    {
        Vector3 inputMainPosition = InputManagerPlayer1.MainJoystick();
        changePos = Vector3.zero;
        changePos.x = inputMainPosition.x;
        changePos.y = inputMainPosition.y;
        isWalking = (changePos.x != 0 || changePos.y != 0);
    }

    public override void MainController()
    {
        if ((InputManagerPlayer1.YButton() || InputManagerPlayer1.BButton()) && currentState == ObjectState.carry)
            StartCoroutine(ThrowObject());
        if (InputManagerPlayer1.YButton() && currentState != ObjectState.attack)
            StartCoroutine(MainAttack());
        else if (changePos != Vector3.zero && currentState != ObjectState.attack)
            MoveObject();
        else
        {
            if (!hasManyForce)
                Decceleration();
            AnimationIdle(); 
        }
    }

    private IEnumerator MainAttack()
    {
        DiagonalAttack();
        RefreshAttackCollider();
        anime.SetBool("Attacking", true);
        currentState = ObjectState.attack;
        GetComponent<AudioManager>().CallAudio("attack");
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

        myObject.GetComponent<CarryObjectManager>().CarryObject(this.gameObject);
    }

    private IEnumerator ThrowObject()
    {
        currentState = ObjectState.idle;
        yield return new WaitForSeconds(0.5f);
    }

    private void RefreshAttackCollider()
    {
        foreach (Transform child in transform){
            child.GetComponent<PolygonCollider2D>().enabled = false;
        }

        foreach (Transform child in transform){
            child.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
}