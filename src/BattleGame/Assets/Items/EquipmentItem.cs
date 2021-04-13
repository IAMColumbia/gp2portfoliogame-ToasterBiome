using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EquipmentItem", menuName = "Items/EquipmentItem")]
public class EquipmentItem : Item
{
    public enum EquipmentType
    {
        Hand,
        Head,
        Chest,
        Pants
    }

    public EquipmentType slot;
    public int atk_bonus;
    public int def_bonus;
    public int matk_bonus;
    public int mdef_bonus;
}
