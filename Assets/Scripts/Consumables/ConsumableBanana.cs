using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableBanana : MonoBehaviour
{
    private float _thrust = 8f;
    private float _knockBackTime = .5f;

    public float Thrust {
        get { return _thrust; }
    }

    public float KnockBackTime {
        get { return _knockBackTime; }
    }
}
