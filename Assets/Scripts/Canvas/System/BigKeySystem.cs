using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigKeySystem : MonoBehaviour
{
    private bool _hasBigKey;
    public event EventHandler OnDecrease;
    public event EventHandler OnIncrease;

    public BigKeySystem(bool bigKeyInit){
        HasBigKey = bigKeyInit;
    }

    public bool HasBigKey {
        get => _hasBigKey;
        set => _hasBigKey = value;
    }

    public void AddBigKey(){
        HasBigKey = true;
        OnIncrease(this, EventArgs.Empty);
    }

    public void RemoveBigKey(){
        HasBigKey = false;
        OnDecrease(this, EventArgs.Empty);
    }
}
