using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private TestInteractionFront _front = null;
    private GameObject           _player = null;
    
    void Start() {
        Front  = transform.parent.Find("Interaction").Find("Front").GetComponent<TestInteractionFront>();
        Player = transform.parent.transform.parent.gameObject;
    }

    public void ActionsList()
    {
        if (Front.ObjectOpen){
            GameObject objectOpen = Front.ObjectOpen;
            objectOpen.GetComponent<OpenObject>().TryToOpen(Player);
        }
    }

    public TestInteractionFront Front {
        get => _front;
        set => _front = value;
    }

    public GameObject Player {
        get => _player;
        set => _player = value;
    }
}
