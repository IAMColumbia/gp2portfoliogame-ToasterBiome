using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConsumableItem", menuName = "Items/ConsumableItem")]
public class ConsumableItem : Item
{
    public int restoreHP;
    public int restoreMP;
    public void OnUse(BattleParticipant target)
    {
        target.HP += restoreHP;
        target.MP += restoreMP;
    }
}
