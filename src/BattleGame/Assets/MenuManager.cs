using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject menuCharacterContainer;
    public List<MenuCharacter> menuCharacters;
    public GameObject menuCharacterPrefab;

    public BattleAbility abilityUseAbility;
    public BattleParticipant abilityUseCaster;

    public void SetupCharacters()
    {
        foreach(BattleParticipant battler in PlayerManager.instance.party)
        {
            GameObject menuCharacter = Instantiate(menuCharacterPrefab, menuCharacterContainer.transform);
            menuCharacter.GetComponent<MenuCharacter>().SetBattler(this, battler);
            menuCharacters.Add(menuCharacter.GetComponent<MenuCharacter>());
        }
    }

    public void RefreshCharacters()
    {
        foreach(MenuCharacter mCharacter in menuCharacters)
        {
            mCharacter.RefreshUI();
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!BattleManager.instance.inBattle) ToggleMenu();

        }
    }

    void ToggleMenu()
    {
        menuObject.SetActive(!menuObject.activeInHierarchy);
        if(menuObject.activeInHierarchy)
        {
            if(menuCharacters.Count == 0)
            {
                SetupCharacters();
            } else
            {
                RefreshCharacters();
            }
            
        }
    }
    public void AbilityPressed(BattleParticipant caster, BattleAbility ability)
    {
        abilityUseCaster = caster;
        abilityUseAbility = ability;
        foreach(MenuCharacter mCharacter in menuCharacters)
        {
            mCharacter.GetComponent<Button>().enabled = true;
            mCharacter.GetComponent<Image>().color = Color.yellow;
        }
    }

    public void AbilityButtonPressed()
    {
        //make the buttons annoyingly selectable
        foreach (MenuCharacter mCharacter in menuCharacters)
        {
            mCharacter.ToggleAbilityButtons(true);
        }
    }

    public void InventoryButtonPressed()
    {
        //just open da inventory lol!!!
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    public void UseAbilityOnCharacter(BattleParticipant target)
    {
        abilityUseCaster.UseAbility(target, abilityUseAbility);
        foreach (MenuCharacter mCharacter in menuCharacters)
        {
            mCharacter.ToggleAbilityButtons(false);
            mCharacter.GetComponent<Button>().enabled = false;
            mCharacter.GetComponent<Image>().color = Color.white;
        }
        abilityUseCaster = null;
        abilityUseAbility = null;
    }
}
