using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateResolveOrder : BattleState
{
    int chosenAction;
    BattleParticipant target;
    public BattleStateResolveOrder(BattleManager bm, int chosenAction, BattleParticipant target) : base(bm)
    {
        this.chosenAction = chosenAction;
        this.target = target;
    }
    public override IEnumerator Start()
    {
        MessageBox.instance.setText($"{bm.battle.activeParticipant.name} attacks {target.name}");
        yield return new WaitForSeconds(2f);
        if (chosenAction == 0)
        {
            if (target != null)
            {
                bm.battle.activeParticipant.Attack(target);
            }

        }
        else
        {
            if (target != null)
            {
                bm.battle.activeParticipant.UseAbility(target, bm.battle.activeParticipant.abilities[chosenAction - 1]);
            }

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
