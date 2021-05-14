using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldChest : OverworldObject
{
    public ItemStack item;
    public bool opened;

    [SerializeField]
    public Sprite closedChest; //unused 
    public Sprite openChest;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteract()
    {
        if(!opened)
        {
            //give them the item
            opened = true;
            sr.sprite = openChest;
            PlayerManager.instance.inventory.Add(item);
            //give a notification?
            MessageBox.instance.setText($"Got {item.item.name} x{item.amount}!",3f);
        }
        

    }
}
