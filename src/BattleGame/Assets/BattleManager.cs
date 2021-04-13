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

    public BattleState battleState;

    public List<ItemStack> bag;

    public void SetState(BattleState state)
    {
        battleState = state;
        StartCoroutine(state.Start());
    }

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
        
    }

    public void StartBattle()
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
        battle.activeParticipant = battle.participants[0];
        SetState(new BattleStateStart(this));
    }

    public void StartBattle(BattlePool startingPool)
    {
        List<BattleParticipant> pool = new List<BattleParticipant>();
        foreach (BattleParticipantTemplate battlerTemplate in startingPool.pool)
        {
            BattleParticipant participant = Instantiate(battlerTemplate).participant;
            pool.Add(participant);
        }
        foreach (BattleParticipantTemplate battlerTemplate in startPool)
        {
            BattleParticipant participant = Instantiate(battlerTemplate).participant;
            pool.Add(participant);
        }
        battle = new Battle(pool);
        SetupUI();
        RefreshUI();
        battle.activeParticipant = battle.participants[0];
        
        SetState(new BattleStateStart(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshUI()
    {
        
    }

    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        battle.currentPhase = Battle.BattlePhase.GiveOrder;
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
