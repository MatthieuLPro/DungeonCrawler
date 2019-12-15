using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _checkPlayerDetection();
    }

    void _checkPlayerDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_rigidbody2D.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
                Debug.Log("Detect Player: " + collider.tag);
        }
    }


}
