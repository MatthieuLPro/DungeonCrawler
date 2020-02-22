using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigKeySystem : MonoBehaviour
{
    private bool _bigKey;
    public event EventHandler OnDecrease;
    public event EventHandler OnIncrease;

    public BigKeySystem(bool bigKeyInit){
        _bigKey = bigKeyInit;
    }

    public bool GetValue(){
        return _bigKey;
    }

    public void AddBigKey()
    {
        _GetBigKey();
        OnIncrease(this, EventArgs.Empty);
    }

    public void RemoveBigKey()
    {
        _LooseBigKey();
        OnDecrease(this, EventArgs.Empty);
    }

    private void _LooseBigKey(){
        _bigKey = false;
    }

    private void _GetBigKey(){
        _bigKey = true;
    }
}
