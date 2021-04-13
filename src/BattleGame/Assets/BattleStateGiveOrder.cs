using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateGiveOrder : BattleState
{
    int chosenAction;
    ItemStack chosenItem;
    public BattleStateGiveOrder(BattleManager bm) : base(bm) { }
    public override IEnumerator Start()
    {
        foreach (BattleParticipantComponent comp in bm.battlerContainers)
        {
            if (comp.participant == bm.battle.activeParticipant)
            {
                comp.selection.enabled = true;
                comp.selection.color = Color.green;
            }
        }

        if (bm.battle.activeParticipant.FACTION != BattleParticipant.Faction.Player)
        {
            bm.SetState(new BattleStateResolveOrder(bm, 0, bm.battle.getRandomEnemy(BattleParticipant.Faction.Player),chosenItem));
            yield break;
        }
        MessageBox.instance.setText("Please choose your action");
        CommandBox.instance.AttackButton.interactable = true;
        CommandBox.instance.AbilityButton.interactable = true;
        CommandBox.instance.ItemButton.interactable = true;

        //show selector

        //now we wait for input
        yield break;
    }

    public override IEnumerator AttackButtonPressed()
    {
        //cool lets enable the targetting
        CommandBox.instance.EnableTargetting(BattleParticipant.Faction.Enemy);
        CommandBox.instance.AttackButton.interactable = false;
        CommandBox.instance.AbilityButton.interactable = false;
        CommandBox.instance.ItemButton.interactable = false;
        yield break;
    }

    public override IEnumerator ItemButtonPressed()
    {
        CommandBox.instance.AttackButton.interactable = false;
        CommandBox.instance.AbilityButton.interactable = false;
        CommandBox.instance.ItemButton.interactable = false;
        Debug.Log("help");
        //selection for using an item
        ItemBox.instance.Open();
        
        yield break;
    }

    public override IEnumerator ItemPressed(ItemStack item)
    {
        chosenAction = -1;
        chosenItem = item;
        CommandBox.instance.EnableTargetting(BattleParticipant.Faction.Player);
        MessageBox.instance.setText($"Use {item.item.name} on who?");
        yield break;
    }

    public override IEnumerator AbilityButtonPressed()
    {
        //enable ability buttons
        CommandBox.instance.SetAbilityButtons(true);
        CommandBox.instance.AttackButton.interactable = false;
        CommandBox.instance.AbilityButton.interactable = false;
        CommandBox.instance.ItemButton.interactable = false;
        yield break;
    }

    public override IEnumerator AbilityPressed(int num)
    {
        chosenAction = num;
        //now we enable targetting
        switch (bm.battle.activeParticipant.abilities[num - 1].targetType)
        {
            case BattleAbility.TargetType.Self:
                CommandBox.instance.EnableTargetting(BattleParticipant.Faction.Player);
                break;
            case BattleAbility.TargetType.Ally:
                CommandBox.instance.EnableTargetting(BattleParticipant.Faction.Player);
                break;
            case BattleAbility.TargetType.Enemy:
                CommandBox.instance.EnableTargetting(BattleParticipant.Faction.Enemy);
                break;
            case BattleAbility.TargetType.AllyOrSelf:
                CommandBox.instance.EnableTargetting(BattleParticipant.Faction.Player);
                break;
            case BattleAbility.TargetType.AllAllies:
                CommandBox.instance.EnableTargetting(BattleParticipant.Faction.Enemy);
                break;
            case BattleAbility.TargetType.AllEnemies:
                CommandBox.instance.EnableTargetting(BattleParticipant.Faction.Enemy);
                break;
            default:
                break;
        }
        
        yield break;
    }

    public override IEnumerator TargetPressed(BattleParticipant target)
    {
        CommandBox.instance.DisableTargetting(target.FACTION);
        CommandBox.instance.SetAction();

        bm.SetState(new BattleStateResolveOrder(bm, chosenAction, target, chosenItem));
        yield break;
    }
}
