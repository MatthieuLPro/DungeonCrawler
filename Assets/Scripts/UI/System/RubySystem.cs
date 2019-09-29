using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RubySystem
{
    public event EventHandler OnDecrease;
    public event EventHandler OnIncrease;

    private int _ruby;

    public RubySystem(int rubyInit){
        _ruby = rubyInit;
    }

    public int GetValue(){
        return _ruby;
    }

    public void ChangeRuby(int amount)
    {
        if (amount < 0)
            DecreaseRuby(amount);
        else
            IncreaseRuby(amount);
        if (OnDecrease != null) OnDecrease(this, EventArgs.Empty);
        if (OnIncrease != null) OnIncrease(this, EventArgs.Empty);
    }

    private void DecreaseRuby(int amount)
    {
        _ruby -= amount;
        if(amount <= 0)
            _ruby = 0;
    }

    private void IncreaseRuby(int amount){
        _ruby += amount;
    }
}
