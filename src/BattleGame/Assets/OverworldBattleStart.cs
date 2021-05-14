using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldBattleStart : OverworldObject
{
    public BattlePool pool;
    // Start is called before the first frame update
    public override void OnInteract()
    {
        OverworldManager.instance.StartBattle(pool);
        Destroy(gameObject);
    }
}
