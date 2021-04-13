using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState
{
    protected BattleManager bm;
    public BattleState(BattleManager bm)
    {
        this.bm = bm;
    }
    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator End()
    {
        yield break;
    }

    public virtual IEnumerator AttackButtonPressed()
    {
        yield break;
    }

    public virtual IEnumerator AbilityButtonPressed()
    {
        yield break;
    }

    public virtual IEnumerator ItemButtonPressed()
    {
        yield break;
    }

    public virtual IEnumerator ItemPressed(ItemStack item)
    {
        yield break;
    }

    public virtual IEnumerator AbilityPressed(int num)
    {
        yield break;
    }

    public virtual IEnumerator TargetPressed(BattleParticipant target)
    {
        yield break;
    }
}
