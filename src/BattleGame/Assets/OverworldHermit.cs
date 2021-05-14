using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldHermit : OverworldSimpleDialogue
{

    public override void OnInteract()
    {
        if (!currentlyTalking && !PlayerManager.instance.canWalkOnLava)
        {
            currentlyTalking = true;
            TextboxController.instance.SendTexts(this);
            PlayerManager.instance.canWalkOnLava = true;
        } else if (!currentlyTalking) 
        {
            TextboxController.instance.SendText($"<color=#FFFF00>Wandering Hermit</color>: Good luck!");
        }
    }
}
