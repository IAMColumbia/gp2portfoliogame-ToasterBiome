using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateResolveOrder : BattleState
{
    int chosenAction;
    BattleParticipant target;
    ItemStack usedItem;

    public BattleStateResolveOrder(BattleManager bm, int chosenAction, BattleParticipant target, ItemStack usedItem) : base(bm)
    {
        this.chosenAction = chosenAction;
        this.target = target;
        this.usedItem = usedItem;
    }



    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        if (chosenAction == 0)
        {
            //Attack
            if (target != null)
            {
                bm.battle.activeParticipant.Attack(target);
            }

        }
        else if (chosenAction > 0)
        {
            //Ability
            if (target != null)
            {
                //need a better way to handle this
                if(bm.battle.activeParticipant.abilities[chosenAction - 1].targetType == BattleAbility.TargetType.AllEnemies)
                {
                    foreach(BattleParticipant participant in bm.battle.participants)
                    {
                        if(participant.FACTION == BattleParticipant.Faction.Enemy)
                        {
                            bm.battle.activeParticipant.UseAbility(participant, bm.battle.activeParticipant.abilities[chosenAction - 1]);
                        }
                    }
                } else
                {
                    bm.battle.activeParticipant.UseAbility(target, bm.battle.activeParticipant.abilities[chosenAction - 1]);
                }
                
            }

        } else if (chosenAction == -1)
        {
            //Item
            if(target != null)
            {
                bm.battle.activeParticipant.UseItem(target, usedItem);
            }
        } else
        {
            Debug.Log("chosenAction was invalid");
        }

        yield return new WaitForSeconds(1f);
        foreach (BattleParticipantComponent comp in bm.battlerContainers)
        {
            if (comp.participant == bm.battle.activeParticipant)
            {
                comp.selection.enabled = false;
                comp.selection.color = Color.white;
            }
        }
        //now check if entire team is dead
        if (bm.battle.getFactionDeath(BattleParticipant.Faction.Player))
        {
            bm.SetState(new BattleStateEnd(bm, BattleParticipant.Faction.Enemy));
            yield break;
        }

        if (bm.battle.getFactionDeath(BattleParticipant.Faction.Enemy))
        {
            bm.SetState(new BattleStateEnd(bm, BattleParticipant.Faction.Player));
            yield break;
        }

        //if none of these happen, go to the next turn
        bm.battle.activeParticipant = bm.battle.participants[(bm.battle.participants.IndexOf(bm.battle.activeParticipant) + 1) % bm.battle.participants.Count];
        //if that is dead, go through a loop to find the next person..
        while(bm.battle.activeParticipant.HP <= 0)
        {
            bm.battle.activeParticipant = bm.battle.participants[(bm.battle.participants.IndexOf(bm.battle.activeParticipant) + 1) % bm.battle.participants.Count];
        }
        bm.SetState(new BattleStateGiveOrder(bm));

        yield break;
    }
}
