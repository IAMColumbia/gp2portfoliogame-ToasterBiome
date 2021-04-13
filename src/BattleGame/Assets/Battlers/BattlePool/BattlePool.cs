using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battler/Pool Template")]
public class BattlePool : ScriptableObject
{
    public List<BattleParticipantTemplate> pool;
}
