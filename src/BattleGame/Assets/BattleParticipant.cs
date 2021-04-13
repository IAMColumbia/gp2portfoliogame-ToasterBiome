using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleParticipant
{
    public string name;

    [SerializeField]
    private int _hp;
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
            {
                _hp = 0;
            } else if (value > MAX_HP) {
                _hp = MAX_HP;
            } else
            {
                _hp = value;
            }
        }
    }
    public int MAX_HP;

    public int MP;
    public int MAX_MP;

    public int ATTACK;
    public int DEFENSE;
    public int MAGICATTACK;
    public int MAGICDEFENSE;
    public int SPEED;

    public List<BattleAbility> abilities;

    public List<EquipmentItem> equipment;

    public Sprite sprite;

    public Faction FACTION;

    public enum Faction
    {
        Player, Enemy
    }

    public BattleParticipant(string name,int maxhp, int maxmp, int atk, int def, int matk, int mdef, int spd, Faction fac, List<BattleAbility> abilities)
    {
        this.name = name;
        this.MAX_HP = maxhp;
        this.HP = maxhp;
        this.MAX_MP = maxmp;
        this.MP = maxmp;
        this.ATTACK = atk;
        this.DEFENSE = def;
        this.MAGICATTACK = matk;
        this.MAGICDEFENSE = mdef;
        this.SPEED = spd;
        this.FACTION = fac;
        this.abilities = abilities; 
    }

    public void Attack(BattleParticipant target)
    {
        int power = (this.ATTACK - target.DEFENSE);
        power = Mathf.Max(0, power);
        MessageBox.instance.setText($"{name} attacks {target.name} for {power} damage!");
        target.HP -= power;
    }

    public void UseAbility(BattleParticipant target, BattleAbility ability)
    {
        if(this.MP >= ability.cost)
        {
            int power = (this.MAGICATTACK + ability.damage) - target.MAGICDEFENSE;
            power = Mathf.Max(0, power);
            MessageBox.instance.setText($"{name} uses {ability.abilityName} on {target.name} for {power} damage!");
            target.HP -= power;
        } else
        {
            MessageBox.instance.setText($"{name} uses {ability.abilityName} on {target.name}! It failed!");
        }
        
    }

    public void UseItem(BattleParticipant target, ItemStack item)
    {
        MessageBox.instance.setText($"{name} uses {item.item.name} on {target.name}!");
        if(item.item.GetType() == typeof(ConsumableItem))
        {
            ConsumableItem cItem = (ConsumableItem)item.item;
            target.HP += cItem.restoreHP;
            target.MP += cItem.restoreMP;
        }
        item.amount -= 1;
        
    }
}
