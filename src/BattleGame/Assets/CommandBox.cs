using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommandBox : MonoBehaviour
{
    public static CommandBox instance;
    public Button AttackButton;
    public Button AbilityButton;

    public List<Button> abilityButtons;
    public List<TextMeshProUGUI> abilityTexts;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Debug.Log("A CommandBox already exists");
            Destroy(gameObject);
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

    public void AttackButtonPressed()
    {
        BattleManager.instance.chosenAbility = 0;
        AttackButton.interactable = false;
        AbilityButton.interactable = false;
        SetAbilityButtons(false);
    }

    public void AbilityPressed(int num)
    {
        Debug.Log("pressed " + num);
        BattleManager.instance.chosenAbility = num;
        AttackButton.interactable = false;
        AbilityButton.interactable = false;
        SetAbilityButtons(false);
    }

    public void SetAbilityButtons(bool active)
    {
        foreach(Button button in abilityButtons)
        {
            button.interactable = active;
        }
    }
}
