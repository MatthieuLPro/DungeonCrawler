using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsequenceDamage : MonoBehaviour, IConsequenceDamage
{
    public ConsequenceDamage();

    // Change Enemy into universal object
    public void DamageOnObject(Enemy enemy, float strength){
        _enemy.ActualHealth = strength * -1;
    }

    public void CallInvincibleTime(GameObject user, SpriteRenderer sprite, float invincibleTime){
        StartCoroutine(_InvincibleTimeCo(user, sprite, invincibleTime));
    }

    private IEnumerator _InvincibleTimeCo(GameObject user, SpriteRenderer sprite, float invincibleTime)
    {
        float time         = .0f;
        Color regularColor = sprite.color;

        while(time < invincibleTime)
        {
            sprite.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(0.02f);

            sprite.color = regularColor;
            yield return new WaitForSeconds(0.02f);

            time += 0.2f;
        }

        sprite.color = regularColor;

        user.GetComponent<UserMonsterInteraction>().KnockToggleParam(false);
    }
}
