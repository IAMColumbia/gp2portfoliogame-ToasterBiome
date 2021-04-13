using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/New Ability")]
public class BattleAbility : ScriptableObject
{
    public enum AbilityType
    {
        Physical, Magic
    }
    public enum TargetType
    {
        Self, Ally, Enemy, AllyOrSelf, AllAllies, AllEnemies
    }

    public string abilityName;
    public AbilityType abilityType;
    public TargetType targetType;
    public int cost;
    public int damage;

}

