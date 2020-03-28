using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player"))
            return;
        ConsumableUI consumablePlayerUI = other.transform.parent.Find("UI").Find("Consumable").GetComponent<ConsumableUI>();
        UseConsumable consumablePlayer = other.transform.Find("UseConsumable").GetComponent<UseConsumable>();

        if (consumablePlayerUI.ConsumableExist())
            return;

        int consumable  = _GenerateConsumable();

        consumablePlayerUI.AddConsumable(consumable);
        consumablePlayer.SetConsumable(consumable);
        _SetUsedSprite();
        Destroy(GetComponent<BoxCollider2D>());
    }

    int _GenerateConsumable() {
        int value = Random.Range(1, 16);

        if (value < 9)
            return 1;
        else
            return 12;
    }

    void _SetUsedSprite() {
        Object[] sprites = Resources.LoadAll("Sprites/Hud/consumables/object_consumable");
        GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[18];
    }

}
