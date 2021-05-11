using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSimpleDialogue : MonoBehaviour
{
    public string npc_name;
    public Color npc_color = Color.white;
    public List<string> messages;
    public int currentIndex = 0;
    public bool currentlyTalking = false;

    public bool containsQuestion = false;

    public string questionMessage;

    public string option1;
    public string option2;

    public string response1;
    public string response2;
    // Start is called before the first frame update
    public string GetSpeech(int index)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(npc_color)}>{npc_name}</color>: {messages[index]}";
    }
    
    public string GetNextSpeech()
    {
        return GetSpeech(currentIndex);
    }
}
