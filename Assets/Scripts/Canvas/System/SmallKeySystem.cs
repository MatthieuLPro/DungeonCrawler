using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallKeySystem : MonoBehaviour
{
    public event EventHandler OnDecrease;
    public event EventHandler OnIncrease;

    private int _SmallKey;

    public SmallKeySystem(int smallKeyInit){
        _SmallKey = smallKeyInit;
    }

    public int GetValue(){
        return _SmallKey;
    }

    public void AddSmallKey()
    {
        IncreaseSmallKey();
        OnIncrease(this, EventArgs.Empty);
    }

    public void RemoveSmallKey()
    {
        DecreaseSmallKey();
        OnDecrease(this, EventArgs.Empty);
    }

    private void DecreaseSmallKey()
    {
        _SmallKey -= 1;
        if(_SmallKey <= 0)
            _SmallKey = 0;
    }

    private void IncreaseSmallKey(){
        _SmallKey += 1;
    }
}
