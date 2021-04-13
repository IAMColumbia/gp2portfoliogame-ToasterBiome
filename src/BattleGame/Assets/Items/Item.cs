using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    //already has a "name".
    public Quality quality;
    public string description;
    public Sprite sprite;


    public enum Quality
    {
        Normal, Uncommon, Rare, UltraRare, Legendary, Mythical
    }

    public Color getColorFromRarity()
    {
        switch (quality)
        {
            case Quality.Normal:
                return Color.white;

            case Quality.Uncommon:
                return new Color(0.207f, 0.992f, 0.317f);

            case Quality.Rare:
                return new Color(0.184f, 0.235f, 0.976f);

            case Quality.UltraRare:
                return new Color(0.839f, 0.4f, 0.917f);

            case Quality.Legendary:
                return new Color(0.996f, 0.831f, 0.301f);
            case Quality.Mythical:
                return new Color(1, 0.019f, 0.066f);
            default:
                return Color.white;
        }
    }
}

    