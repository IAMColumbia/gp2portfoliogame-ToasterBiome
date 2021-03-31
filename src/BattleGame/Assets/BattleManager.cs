using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public List<BattleAbility> heroAbility;
    public List<BattleAbility> enemyAbility;
    
    public Battle battle;

    public int chosenAbility = -1;

    public Slider heroHP;
    public Slider heroMP;
    public Slider enemyHP;
    public Slider enemyMP;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("A BattleManager already exists");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        List<BattleParticipant> pool = new List<BattleParticipant>();

        BattleParticipant hero = new BattleParticipant("Hero", 50, 20, 5, 3, 1, 2, 5,BattleParticipant.Faction.Player,heroAbility);
        BattleParticipant slime = new BattleParticipant("Goblin", 10, 5, 4, 2, 1, 2, 4, BattleParticipant.Faction.Enemy,enemyAbility);

        pool.Add(hero);
        pool.Add(slime);

        battle = new Battle(pool);
        SetupUI();
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        switch (battle.currentPhase)
        {
            case Battle.BattlePhase.StartBattle:
                battle.SortBySpeed();
                battle.activeParticipant = battle.participants[0]; //first guy is fastest, he goes first
                battle.currentPhase = Battle.BattlePhase.GiveOrder;
                break;
            case Battle.BattlePhase.GiveOrder:
                if (battle.activeParticipant.FACTION == BattleParticipant.Faction.Player)
                {
                    //player controlled
                    CommandBox.instance.AttackButton.interactable = true;
                    CommandBox.instance.AbilityButton.interactable = true;
                    //wait for player input
                    if(chosenAbility != -1)
                    {
                        if (chosenAbility == 0)
                        {
                            //normal attack
                            battle.activeParticipant.Attack(battle.getRandomEnemy(BattleParticipant.Faction.Enemy));
                        }
                        else
                        {
                            //ability attack
                            battle.activeParticipant.UseAbility(battle.getRandomEnemy(BattleParticipant.Faction.Enemy), battle.activeParticipant.abilities[chosenAbility - 1]);
                        }
                        chosenAbility = -1; //reset it back
                        battle.currentPhase = Battle.BattlePhase.ExecuteOrder;
                    }

                }
                else
                {
                    //ai controlled, will only attack for now?
                    battle.activeParticipant.Attack(battle.getRandomEnemy(BattleParticipant.Faction.Player));
                    battle.currentPhase = Battle.BattlePhase.ExecuteOrder;
                }

                break;
            case Battle.BattlePhase.ExecuteOrder:
                RefreshUI();
                bool dead = false;
                //check if someone dead af
                foreach(BattleParticipant participant in battle.participants)
                {
                    if(participant.HP == 0)
                    {
                        Debug.Log("Someone dead!");
                        dead = true;
                        break;
                    }
                }
                if(dead)
                {
                    battle.currentPhase = Battle.BattlePhase.EndBattle;
                    break;
                }

                //if not, get the next battler
                battle.activeParticipant = battle.participants[(battle.participants.IndexOf(battle.activeParticipant) + 1) % battle.participants.Count];
                battle.currentPhase = Battle.BattlePhase.GiveOrder;
                
                break;
            case Battle.BattlePhase.EndBattle:
                //You win! Nothing happens.
                MessageBox.instance.setText("You won! Press ESC to exit or Space to reset");
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(0);
                }
                break;
            default:
                break;
        }
    }

    public void RefreshUI()
    {
        heroHP.value = ((float)battle.participants[0].HP / (float)battle.participants[0].MAX_HP);
        heroMP.value = ((float)battle.participants[0].MP / (float)battle.participants[0].MAX_MP);
        enemyHP.value = ((float)battle.participants[1].HP / (float)battle.participants[1].MAX_HP);
        enemyMP.value = ((float)battle.participants[1].MP / (float)battle.participants[1].MAX_MP);
    }

    public void SetupUI()
    {
        BattleParticipant hero = battle.participants[0];

        for (int i = 0; i < hero.abilities.Count; i++)
        {
            CommandBox.instance.abilityTexts[i].text = hero.abilities[i].abilityName;
        }
    }
}
