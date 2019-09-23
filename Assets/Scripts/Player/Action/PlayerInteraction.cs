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
            interactObject.GetComponent<Pot>().OpenThePot();
            GetComponent<PlayerController>().CarryObject(temp);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (RigidBodyVerification(other) == false)
            return;
        
        interactObject = other.gameObject;
        _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (RigidBodyVerification(other) == false)
            return;
        
        interactObject = null;
        _playerInRange = false;
        
    }

    private bool RigidBodyVerification(Collider2D other)
    {
        if (!other.CompareTag("Pot"))
            return false;

        Rigidbody2D pot = other.GetComponent<Rigidbody2D>();
        if (pot != null)
            return false;
        
        return true;
    }
}
