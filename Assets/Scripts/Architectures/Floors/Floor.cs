using UnityEngine;

abstract public class Floor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.Find("MovementTest"))
            return;

        TestMovement objectMovement = other.transform.Find("MovementTest").GetComponent<TestMovement>();
        newMovement(objectMovement);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.Find("MovementTest"))
            return;

        TestMovement objectMovement = other.transform.Find("MovementTest").GetComponent<TestMovement>();
        oldMovement(objectMovement);
    }

    protected abstract void newMovement(TestMovement objectMovement);
    protected abstract void oldMovement(TestMovement objectMovement);
}
