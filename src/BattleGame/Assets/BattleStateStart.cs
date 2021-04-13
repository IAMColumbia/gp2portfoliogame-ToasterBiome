using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateStart : BattleState
{
    public BattleStateStart(BattleManager bm) : base(bm) { }
    public override IEnumerator Start()
    {
        MessageBox.instance.setText("A new battle has started!");

        yield return new WaitForSeconds(2f);

        //set state to giving orders state
        bm.SetState(new BattleStateGiveOrder(bm));
        yield break;
    }
}
