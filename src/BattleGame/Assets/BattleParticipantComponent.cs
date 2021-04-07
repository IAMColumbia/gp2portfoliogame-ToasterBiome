using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleParticipantComponent : MonoBehaviour
{
    public BattleParticipant participant;
    public Slider hpSlider;
    public Slider mpSlider;
    public Image sprite;
    public Image selection;
    public Button selectionButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = ((float)participant.HP / (float)participant.MAX_HP);
        mpSlider.value = ((float)participant.MP / (float)participant.MAX_MP);
    }

    public void setParticipant(BattleParticipant part)
    {
        this.participant = part;
        this.sprite.sprite = part.sprite;
        gameObject.name = participant.name;
    }

    public void enableSelection()
    {
        selection.enabled = true;
        selectionButton.enabled = true;
    }

    public void disableSelection()
    {
        selection.enabled = false;
        selectionButton.enabled = false;
    }

    public void OnTargetPressed()
    {
        CommandBox.instance.SetTarget(participant);
    }
}
