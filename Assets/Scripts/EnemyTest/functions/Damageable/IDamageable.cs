using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int ActualHealth { get; set; }
    int GetNewValue(int actualHealth, int value);
    int GetValueFromLimits(int variable, int limit);
}
