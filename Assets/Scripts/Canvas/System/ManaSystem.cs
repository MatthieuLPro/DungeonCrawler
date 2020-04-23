using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem
{
    public event EventHandler OnDecrease;
    public event EventHandler OnIncrease;

    private Mana _mana;

    public ManaSystem(int manaInit){
        _mana = new Mana(manaInit);
    }

    public int GetMana(){
        return _mana.GetValue();
    }

    public int GetManaMax(){
        return _mana.GetMaxValue();
    }

    public void ChangeMana(int point)
    {
        int upOrDown = 0;

        if (point > 0) upOrDown = 1;

        point = Math.Abs(point);
        while(point > 0)
        {
            if (upOrDown == 0)
            {
                if (DecreaseMana()) break;
            }
            else
                if (IncreaseMana()) break;
            point--;
        }
        if (OnDecrease != null) OnDecrease(this, EventArgs.Empty);
        if (OnIncrease != null) OnIncrease(this, EventArgs.Empty);
    }

    // If a bug appear for mana : add Debug.Log before return false
    private bool DecreaseMana()
    {
        _mana.DecreaseByOne();
        if(_mana.ValueIsOffLimitDown()){
            _mana.SetValue(0);
            return true;
        }
        return false;
    }

    private bool IncreaseMana()
    {
        _mana.IncreaseByOne();
        if(_mana.ValueIsOffLimitUp()){
            int maxValue = _mana.GetMaxValue();
            _mana.SetValue(maxValue);
            return true;
        }
        return false;
    }

    public class Mana
    {
        private int _value;
        private int _maxValue;

        public Mana(int value){
            this._maxValue  = value;
            this._value     = value;
        }

        public int GetValue(){
            return this._value;
        }

        public int GetMaxValue(){
            return this._maxValue;
        }

        public void SetValue(int value){
            this._value = value;
        }

        public void SetMaxValue(int value) {
            this._maxValue = value;
        }

        public void DecreaseByOne(){
            this._value--;
        }

        public void IncreaseByOne(){
            this._value++;
        }

        public bool ValueIsOffLimitDown() {
            if (this._value <= 0)
                return true;
            return false;
        }

        public bool ValueIsOffLimitUp() {
            if (this._value >= this._maxValue)
                return true;
            return false;
        }
    }
}
