using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _playerInRange = false;
    private GameObject interactObject;

    private void Update()
    {
        if (Input.GetButtonDown("Carry") && _playerInRange == true)
        {
            GameObject temp = interactObject;
            if (temp.CompareTag("ObjectCarry"))
            {
                interactObject.GetComponent<CarryObject>().RaiseTheObject();
                GetComponent<PlayerController>().CarryObject(temp);
            }
            else if (temp.CompareTag("ObjectOpen"))
                interactObject.GetComponent<OpenObject>().OpenTheObject();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<Animator>().GetBool("Carrying") == true)
            return;

        if (ObjectVerification(other) == false)
            return;
        
        interactObject = other.gameObject;
        _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (ObjectVerification(other) == false)
            return;
        
        interactObject = null;
        _playerInRange = false;
        
    }

    private bool ObjectVerification(Collider2D other)
    {
        if (other == null)
            return false;

        if ((!other.CompareTag("ObjectCarry") && !other.CompareTag("ObjectOpen")))
            return false;        

        return true;
    }
}
