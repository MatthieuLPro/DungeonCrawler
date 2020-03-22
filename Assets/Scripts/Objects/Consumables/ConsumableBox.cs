using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player"))
            return;
        ConsumableUI consumablePlayer = other.transform.parent.Find("UI").Find("Consumable").GetComponent<ConsumableUI>();

        if (consumablePlayer.ConsumableExist())
            return;

        int consumable  = _GenerateConsumable();

        consumablePlayer.AddConsumable(consumable);
        _SetUsedSprite();
        Destroy(GetComponent<BoxCollider2D>());
    }

    int _GenerateConsumable() {
        return Random.Range(1, 16);
    }

    void _SetUsedSprite() {
        Object[] sprites = Resources.LoadAll("Sprites/Hud/consumables/object_consumable");
        GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[18];
    }
}
