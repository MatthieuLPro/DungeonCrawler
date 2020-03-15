using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : IDamageable
{
    private int _maxHealth;
    private int _actualHealth;

    public Damageable(int maxHealth)
    {
        _maxHealth = maxHealth;
        _actualHealth = _maxHealth;
    }
    
    public int ActualHealth { 
        get { return _actualHealth; } 
        set {
            _actualHealth = GetNewValue(_actualHealth, value);
            _actualHealth = GetValueFromLimits(_actualHealth, _maxHealth); 
        }
    }

    public int GetNewValue(int actualHealth, int value){
        return (actualHealth += value);        
    }

    public int GetValueFromLimits(int variable, int limit)
    {
        if (variable > limit)
            return limit;

        if (variable < 0)
            return 0;
        
        return variable;
    }
}
