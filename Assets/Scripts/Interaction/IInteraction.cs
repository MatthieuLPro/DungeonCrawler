using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    void OnTriggerEnter2D(Collider2D other);
}
