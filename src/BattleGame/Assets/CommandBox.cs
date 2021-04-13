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
    public Button ItemButton;

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
        StartCoroutine(BattleManager.instance.battleState.AttackButtonPressed());
        //BattleManager.instance.chosenAbility = 0;
        chosenAbility = 0;
        AttackButton.interactable = false;
        AbilityButton.interactable = false;
        ItemButton.interactable = false;
        SetAbilityButtons(false);
        EnableTargetting(BattleParticipant.Faction.Enemy);
    }

    public void ItemButtonPressed()
    {
        //StartCoroutine(BattleManager.instance.battleState.AttackButtonPressed());
        //BattleManager.instance.chosenAbility = 0;
        StartCoroutine(BattleManager.instance.battleState.ItemButtonPressed());
    }

    public void AbilityPressed(int num)
    {
        StartCoroutine(BattleManager.instance.battleState.AbilityPressed(num));
        Debug.Log("pressed " + num);
        chosenAbility = num;
        AttackButton.interactable = false;
        AbilityButton.interactable = false;
        ItemButton.interactable = false;
        SetAbilityButtons(false);
        //EnableTargetting(BattleParticipant.Faction.Enemy);
    }

    public void EnableTargetting(BattleParticipant.Faction faction)
    {
        foreach (BattleParticipantComponent comp in BattleManager.instance.battlerContainers)
        {
            if (comp.participant.FACTION == faction)
            {
                comp.enableSelection();
            }
        }
    }

    public void DisableTargetting(BattleParticipant.Faction faction)
    {
        foreach (BattleParticipantComponent comp in BattleManager.instance.battlerContainers)
        {
            if (comp.participant.FACTION == faction)
            {
                comp.disableSelection();
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
        StartCoroutine(BattleManager.instance.battleState.TargetPressed(target));
        this.target = target;
        DisableTargetting(BattleParticipant.Faction.Enemy);
        SetAction();
    }

    public void SetAction()
    {
        BattleManager.instance.target = target;
        BattleManager.instance.chosenAbility = chosenAbility;
    }
}
