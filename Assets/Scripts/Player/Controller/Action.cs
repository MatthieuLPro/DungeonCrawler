﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private TestInteractionFront _front = null;
    private GameObject           _player = null;
    
    void Start() {
        _front = transform.parent.Find("Interaction").Find("Front").GetComponent<TestInteractionFront>();
        _player = transform.parent.transform.parent.gameObject;
    }

    void Update() {
       if (InputManagerPlayer1.AButton() && _front.ObjectOpen)
        {
            Debug.Log("Open");
            GameObject objectOpen = _front.ObjectOpen;
            objectOpen.GetComponent<OpenObject>().TryToOpen(_player);
        }         
    }
}
