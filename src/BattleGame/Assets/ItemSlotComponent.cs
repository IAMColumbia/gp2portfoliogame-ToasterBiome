using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotComponent : MonoBehaviour
{
    public ItemStack itemStack;
    public Button button;
    public Image image;
    public TextMeshProUGUI amountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupItem(ItemStack itemStack)
    {
        this.itemStack = itemStack;
        image.sprite = itemStack.item.sprite;
        amountText.text = itemStack.amount.ToString();
    }

    public void OnButtonPressed()
    {
        ItemBox.instance.OnItemPressed(this);
    }
}
