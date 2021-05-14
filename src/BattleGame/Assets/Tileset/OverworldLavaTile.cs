using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/New Overworld Lava Tile")]
public class OverworldLavaTile : OverworldTile
{
    //just need something special for the lava

    public override bool getPassable()
    {
        return PlayerManager.instance.canWalkOnLava;
    }
}
