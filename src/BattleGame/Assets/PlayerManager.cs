using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public List<BattleParticipant> party;
    public List<BattleParticipantTemplate> partyTemplate;

    public List<ItemStack> inventory;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("A PlayerManager already exists");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        party.Clear();
        foreach(BattleParticipantTemplate template in partyTemplate)
        {
            party.Add(Instantiate(template).participant);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
