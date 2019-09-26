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
            interactObject.GetComponent<CarryObject>().OpenTheObject();
            GetComponent<PlayerController>().CarryObject(temp);
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
        if (!other.CompareTag("ObjectCarry") || other == null)
            return false;
        
        return true;
    }
}
