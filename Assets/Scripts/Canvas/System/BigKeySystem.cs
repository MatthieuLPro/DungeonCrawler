using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigKeySystem : MonoBehaviour
{
    private bool _bigKey;

    public BigKeySystem(bool bigKeyInit){
        _bigKey = bigKeyInit;
    }

    public int GetValue(){
        return _bigKey;
    }

    public void AddSmallKey()
    {
        _GetBigKey();
        OnIncrease(this, EventArgs.Empty);
    }

    public void RemoveSmallKey()
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
