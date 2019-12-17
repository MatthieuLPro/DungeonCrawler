using UnityEngine;

abstract public class Floor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.Find("Movement"))
            return;

        Movement objectMovement = other.transform.GetChild(0).GetComponent<Movement>();
        newMovement(objectMovement);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.Find("Movement"))
            return;

        Movement objectMovement = other.transform.GetChild(0).GetComponent<Movement>();
        oldMovement(objectMovement);
    }

    protected abstract void newMovement(Movement objectMovement);
    protected abstract void oldMovement(Movement objectMovement);
}
