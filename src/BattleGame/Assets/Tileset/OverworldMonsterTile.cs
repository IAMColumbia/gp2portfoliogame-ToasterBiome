using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/New Overworld Monster Tile")]
public class OverworldMonsterTile : OverworldTile
{
    public List<BattlePool> pool;
    public float chance; //between 0 and 1?
    public BattlePool GetRandomPool()
    {

        return pool[Random.Range(0, pool.Count)];
    }
}
