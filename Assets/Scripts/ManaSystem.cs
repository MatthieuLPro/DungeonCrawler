using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem
{
    public event EventHandler OnDecrease;
    public event EventHandler OnIncrease;

    private Mana _mana;

    public ManaSystem(){
        _mana = new Mana(34);
    }

    public int GetMana(){
        return _mana.GetValue();
    }

    public void ChangeMana(int point)
    {
        int upOrDown = 0;

        if (point > 0) upOrDown = 1;

        point = Math.Abs(point);
        while(point > 0)
        {
            if (upOrDown == 0)
                if (DecreaseMana()) break;
            else
                if (IncreaseMana()) break;
            point--;
        }
        if (OnDecrease != null) OnDecrease(this, EventArgs.Empty);
        if (OnIncrease != null) OnIncrease(this, EventArgs.Empty);
    }

    private bool DecreaseMana()
    {
        _mana.DecreaseByOne();
        if(_mana.GetValue() <= 0)
        {
            _mana.SetValue(0);
            return true;
        }
        return false;
    }

    private bool IncreaseMana()
    {
        _mana.IncreaseByOne();
        if(_mana.GetValue() >= 34)
        {
            _mana.SetValue(34);
            return true;
        }
        return false;
    }

    public class Mana
    {
        private int _value;

        public Mana(int value){
            this._value = value;
        }

        public int GetValue(){
            return this._value;
        }

        public void SetValue(int value){
            this._value = value;
        }

        public void DecreaseByOne(){
            this._value--;
        }

        public void IncreaseByOne(){
            this._value++;
        }
    }
}
