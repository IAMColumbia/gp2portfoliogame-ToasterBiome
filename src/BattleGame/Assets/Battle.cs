using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Battle
{
    public List<BattleParticipant> participants;

    public enum BattlePhase
    {
        StartBattle,
        GiveOrder,
        ExecuteOrder,
        Wait,
        EndBattle
    }

    public BattlePhase currentPhase;

    public BattleParticipant activeParticipant;

    public Battle(List<BattleParticipant> participants)
    {
        this.participants = participants;
        currentPhase = BattlePhase.StartBattle;
        SortBySpeed();
    }

    public void SortBySpeed()
    {
        participants = participants.OrderByDescending(b => b.SPEED).ToList();
    }

    public BattleParticipant getRandomEnemy(BattleParticipant.Faction faction)
    {
        List<BattleParticipant> participantsInFaction = new List<BattleParticipant>();
        foreach (BattleParticipant participant in participants)
        {
            if(participant.FACTION == faction)
            {
                participantsInFaction.Add(participant);
            }
        }
        return participantsInFaction[UnityEngine.Random.Range(0, participantsInFaction.Count)];
    }

    public bool getFactionDeath(BattleParticipant.Faction faction)
    {
        foreach (BattleParticipant participant in participants)
        {
            if (participant.FACTION == faction)
            {
                if(participant.HP > 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
