using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuCharacter : MonoBehaviour
{
    public MenuManager manager;
    public TextMeshProUGUI stats;
    public Image sprite;
    public Slider hpSlider;
    public Slider mpSlider;
    public TextMeshProUGUI hpmpBar;
    public TextMeshProUGUI[] abilityButtonsText;
    public Button[] abilityButtons;

    public BattleParticipant battler; //who is this?

    public void SetBattler(MenuManager manager, BattleParticipant battler)
    {
        this.manager = manager;
        this.battler = battler;
        RefreshUI();
    }
    public void RefreshUI()
    {
        sprite.sprite = battler.sprite;
        stats.text = $"Name: {battler.name} \n" +
                       "Class: Hero \n" + 
                       "Level: 7 \n" +
                       "EXP: 235/2444";

        hpSlider.value = ((float)battler.HP / (float)battler.MAX_HP);
        mpSlider.value = ((float)battler.MP / (float)battler.MAX_MP);

        hpmpBar.text = $"{battler.HP}/{battler.MAX_HP} \n" +
                       $"{battler.MP}/{battler.MAX_MP}";

        for(int i = 0; i < battler.abilities.Count; i++)
        {
            if(i < abilityButtons.Length)
            {
                abilityButtonsText[i].text = battler.abilities[i].abilityName;
            }
            
        }
    }

    public void ToggleAbilityButtons(bool active)
    {
        if(active)
        {
            for (int i = 0; i < battler.abilities.Count; i++)
            {
                if (battler.abilities[i].targetType == BattleAbility.TargetType.AllyOrSelf) //only enable the ones we can use..
                {
                    abilityButtons[i].enabled = true;
                    abilityButtons[i].targetGraphic.color = Color.yellow;
                }
            }
        } else
        {
            for (int i = 0; i < battler.abilities.Count; i++)
            {
                abilityButtons[i].enabled = false;
                abilityButtons[i].targetGraphic.color = Color.white;
            }
        }
        
    }

    public void MenuCharacterPressed()
    {
        manager.UseAbilityOnCharacter(this.battler);
    }

    public void AbilityPressed(int num)
    {
        manager.AbilityPressed(battler, battler.abilities[num]);
        abilityButtons[num].targetGraphic.color = Color.green;
    }
}
