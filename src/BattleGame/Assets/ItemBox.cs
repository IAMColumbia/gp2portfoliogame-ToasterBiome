using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public static ItemBox instance;

    public GameObject slotPrefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("An ItemBox already exists");
            Destroy(gameObject);
        }
    }

    public void Open()
    {
        instance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        RefreshUI();
    }

    public void Close()
    {
        instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void RefreshUI()
    {
        foreach(Transform child in transform.GetChild(0).transform)
        {
            Destroy(child.gameObject);
        }
        foreach(ItemStack itemStack in BattleManager.instance.bag)
        {
            GameObject newObject = Instantiate(slotPrefab, transform.GetChild(0).transform);
            newObject.GetComponent<ItemSlotComponent>().SetupItem(itemStack);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemPressed(ItemSlotComponent pressedItem)
    {
        StartCoroutine(BattleManager.instance.battleState.ItemPressed(pressedItem.itemStack));
        Close();
    }
}
