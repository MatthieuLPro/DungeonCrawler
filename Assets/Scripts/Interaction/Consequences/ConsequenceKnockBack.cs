using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsequenceKnockBack : MonoBehaviour, IConsequenceKnockBack
{
    public void CallKnockBack(GameObject user, Vector2 directionKnock, float thrust, float knockBackTime){
        StartCoroutine(_KnockBackTimeCo(user, directionKnock, thrust, knockBackTime));
    }

    private IEnumerator _KnockBackTimeCo(GameObject user, Vector2 position, float thrust, float knockBackTime)
    {
        Vector2 directionKnock  = CalculateKnockBackDirection(position);

        user.GetComponent<UserMonsterInteraction>().KnockToggleParam(true);

        _ApplyThrustOnObject(directionKnock * thrust);        
        _CallHurt();

        yield return new WaitForSeconds(knockBackTime);
    }

    /* ************************************************ */
    /* Apply force functions */
    /* ************************************************ */
    private Vector2 CalculateKnockBackDirection(Vector3 opponentPosition){
        return (_parent.transform.position - opponentPosition);
    }

    private void _ApplyThrustOnObject(Vector3 strengthDirection){
        _parent.GetComponent<Rigidbody2D>().AddForce(strengthDirection, ForceMode2D.Impulse);
    }
}
