using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    void FixedUpdate()
    {
        RaycastHit2D hit0 = Physics2D.Raycast(transform.position, new Vector2(0.4f, 0));
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector2(-0.4f, 0));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(0, 0.4f));
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, new Vector2(0, -0.4f));
        RaycastHit2D hit4 = Physics2D.Raycast(transform.position, new Vector2(0.4f, 0.4f));
        RaycastHit2D hit5 = Physics2D.Raycast(transform.position, new Vector2(-0.4f, 0.4f));
        RaycastHit2D hit6 = Physics2D.Raycast(transform.position, new Vector2(-0.4f, -0.4f));
        RaycastHit2D hit7 = Physics2D.Raycast(transform.position, new Vector2(0.4f, -0.4f));

        Debug.DrawRay(transform.position, new Vector2(0.4f, 0), Color.red);
        Debug.DrawRay(transform.position, new Vector2(-0.4f, 0), Color.red);
        Debug.DrawRay(transform.position, new Vector2(0, 0.4f), Color.red);
        Debug.DrawRay(transform.position, new Vector2(0, -0.4f), Color.red);
        
        Debug.DrawRay(transform.position, new Vector2(0.4f, 0.4f), Color.red);
        Debug.DrawRay(transform.position, new Vector2(-0.4f, 0.4f), Color.red);
        Debug.DrawRay(transform.position, new Vector2(-0.4f, -0.4f), Color.red);
        Debug.DrawRay(transform.position, new Vector2(0.4f, -0.4f), Color.red);

        if (hit0.collider != null)
        {
            Debug.Log("hit0.collider: " + hit0.collider);
            Debug.Log("hit0.distance: " + hit0.distance);
        }
        if (hit1.collider != null)
        {
            Debug.Log("hit1.collider: " + hit1.collider);
            Debug.Log("hit1.distance: " + hit1.distance);
        }
        if (hit2.collider != null)
        {
            Debug.Log("hit2.collider: " + hit2.collider);
            Debug.Log("hit2.distance: " + hit2.distance);
        }
        if (hit3.collider != null)
        {
            Debug.Log("hit3.collider: " + hit3.collider);
            Debug.Log("hit3.distance: " + hit3.distance);
        }
        if (hit4.collider != null)
        {
            Debug.Log("hit4.collider: " + hit4.collider);
            Debug.Log("hit4.distance: " + hit4.distance);
        }
        if (hit5.collider != null)
        {
            Debug.Log("hit5.collider: " + hit5.collider);
            Debug.Log("hit5.distance: " + hit5.distance);
        }
        if (hit6.collider != null)
        {
            Debug.Log("hit6.collider: " + hit6.collider);
            Debug.Log("hit6.distance: " + hit6.distance);
        }
        if (hit7.collider != null)
        {
            Debug.Log("hit7.collider: " + hit7.collider);
            Debug.Log("hit7.distance: " + hit7.distance);
        }
    }
}
