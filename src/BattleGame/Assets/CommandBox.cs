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

    public BattleParticipant target;
    public int chosenAbility;

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
        //BattleManager.instance.chosenAbility = 0;
        chosenAbility = 0;
        AttackButton.interactable = false;
        AbilityButton.interactable = false;
        SetAbilityButtons(false);
        foreach(BattleParticipantComponent comp in BattleManager.instance.battlerContainers)
        {
            if(comp.participant.FACTION == BattleParticipant.Faction.Enemy)
            {
                comp.enableSelection();
            }
            
        }
    }

    public void AbilityPressed(int num)
    {
        Debug.Log("pressed " + num);
        chosenAbility = num;
        AttackButton.interactable = false;
        AbilityButton.interactable = false;
        SetAbilityButtons(false);
        foreach (BattleParticipantComponent comp in BattleManager.instance.battlerContainers)
        {
            if (comp.participant.FACTION == BattleParticipant.Faction.Enemy)
            {
                comp.enableSelection();
            }
        }
    }

    public void SetAbilityButtons(bool active)
    {
        foreach(Button button in abilityButtons)
        {
            button.interactable = active;
        }
    }

    public void SetTarget(BattleParticipant target)
    {
        this.target = target;
        foreach (BattleParticipantComponent comp in BattleManager.instance.battlerContainers)
        {
            comp.disableSelection();
        }
        SetAction();
    }

    public void SetAction()
    {
        BattleManager.instance.target = target;
        BattleManager.instance.chosenAbility = chosenAbility;
    }
}
