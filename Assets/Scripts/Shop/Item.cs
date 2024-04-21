using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        ArmorNone,
        Armor_1,
        Armor_2,
        HelmetNone,
        Helmet,
        HealthPotion,
        Sword_1,
        Sword_2
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.ArmorNone: return 0;
            case ItemType.Armor_1: return 30;
            case ItemType.Armor_2: return 100;
            case ItemType.HelmetNone: return 0;
            case ItemType.Helmet: return 90;
            case ItemType.HealthPotion: return 30;
            case ItemType.Sword_1: return 0;
            case ItemType.Sword_2: return 150;
        }
    }
}
