using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryItem : MonoBehaviour
{
    private GameObject _carrier = null;

    void FixedUpdate()
    {
        if (transform.position != _carrier.transform.position + new Vector3(0, 0.1f, 0))
            transform.position = _carrier.transform.position + new Vector3(0, 0.1f, 0);          
    }

    public void SetCarrier(GameObject carrier){
        _carrier = carrier;
    }
}
