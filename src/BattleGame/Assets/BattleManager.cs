using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public List<BattleParticipantTemplate> startPool;

    public List<BattleParticipantComponent> battlerContainers;
    
    public Battle battle;

    public int chosenAbility = -1;
    public BattleParticipant target;

    public GameObject leftSide;
    public GameObject rightSide;

    public GameObject participantPrefab;

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
        foreach (BattleParticipantTemplate battlerTemplate in startPool)
        {
            BattleParticipant participant = Instantiate(battlerTemplate).participant;
            pool.Add(participant);
        }
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
                    //show who's going?
                    foreach(BattleParticipantComponent comp in battlerContainers)
                    {
                        if(comp.participant == battle.activeParticipant)
                        {
                            comp.selection.enabled = true;
                            comp.selection.color = Color.green;
                        }
                    }

                    //wait for player input
                    if(chosenAbility != -1)
                    {
                        if (chosenAbility == 0)
                        {
                            if(target != null)
                            {
                                battle.activeParticipant.Attack(target);
                            }
                            
                        }
                        else
                        {
                            if (target != null)
                            {
                                battle.activeParticipant.UseAbility(target, battle.activeParticipant.abilities[chosenAbility - 1]);
                            }
                            
                        }
                        
                        chosenAbility = -1; //reset it back
                        target = null;
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
                //reset the current person
                foreach (BattleParticipantComponent comp in battlerContainers)
                {
                    if (comp.participant == battle.activeParticipant)
                    {
                        comp.selection.enabled = false;
                        comp.selection.color = Color.white;
                    }
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
        
    }

    public void SetupUI()
    {
        battlerContainers = new List<BattleParticipantComponent>();
        foreach (BattleParticipant participant in battle.participants)
        {
            GameObject participantObject = Instantiate(participantPrefab);
            if(participant.FACTION == BattleParticipant.Faction.Player)
            {
                participantObject.transform.parent = rightSide.transform;
            } else
            {
                participantObject.transform.parent = leftSide.transform;
            }
            BattleParticipantComponent comp = participantObject.GetComponent<BattleParticipantComponent>();
            comp.setParticipant(participant);
            battlerContainers.Add(comp);
            
        }
        
        BattleParticipant hero = battle.participants[0];

        for (int i = 0; i < hero.abilities.Count; i++)
        {
            CommandBox.instance.abilityTexts[i].text = hero.abilities[i].abilityName;
        }
        
    }
}
