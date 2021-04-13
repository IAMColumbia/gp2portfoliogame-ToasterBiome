using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStateEnd : BattleState
{
    BattleParticipant.Faction winningFaction;
    public BattleStateEnd(BattleManager bm, BattleParticipant.Faction winningFaction) : base(bm) {
        this.winningFaction = winningFaction;
    }
    public override IEnumerator Start()
    {
        if(winningFaction == BattleParticipant.Faction.Player)
        {
            MessageBox.instance.setText("You won! Press escape to go back");
        } else
        {
            MessageBox.instance.setText("You lost! Press escape to go back");
        }

        while(!Input.GetKeyDown(KeyCode.Escape))
        {
            yield return null;
        }

        OverworldManager.instance.EndBattle();

        yield break;
    }
}
