using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextboxController : MonoBehaviour
{

    public static TextboxController instance;

    public GameObject textbox;

    public TextMeshProUGUI text;

    public Coroutine textCO;

    public bool isWriting = false;
    public bool multiMessage = false;
    public OverworldSimpleDialogue dialogue;


    public TextMeshProUGUI option1text;
    public TextMeshProUGUI option2text;

    public GameObject optionBox;
    public bool answered = false;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Debug.LogError("There's already a Textbox Controller");
            Destroy(this);
        }
    }

    IEnumerator MakeText(string input)
    {
        isWriting = true;
        text.text = "";
        while (text.text != input)
        {
            if(input[text.text.Length] == '<')
            {
                //find the >

                int loc = input.IndexOf(">",text.text.Length);

                Debug.Log(loc - text.text.Length);
                string markup = input.Substring(text.text.Length, loc - text.text.Length + 1);
                Debug.Log(markup);
                text.text += markup;

            } else
            {
                text.text += input[text.text.Length];
            }

            //if last letter is special

            char lastChar = input[text.text.Length - 1];
            if(lastChar == '.')
            {
                yield return new WaitForSeconds(0.2f);
            }

            if (lastChar == '?')
            {
                yield return new WaitForSeconds(0.15f);
            }
               
            yield return new WaitForSeconds(0.0125f);


            
        }
        isWriting = false;
    }

    public void SendText(string input)
    {
        if (textCO != null)
        {
            StopCoroutine(textCO);
        }
        OpenDialogueBox();
        textCO = StartCoroutine(MakeText(input));
    }

    public void SendTexts(OverworldSimpleDialogue dialogue)
    {
        //set it to multimessage mode
        multiMessage = true;
        this.dialogue = dialogue;
        dialogue.currentIndex = 0;
        //do the first line
        OpenDialogueBox();
        StartCoroutine(MakeText(dialogue.GetSpeech(dialogue.currentIndex)));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && textbox.activeInHierarchy)
        {
            NextLine();
        }
    }

    public void NextLine()
    {
        if (!isWriting && !multiMessage)
        {
            CloseDialogueBox();
            dialogue.currentlyTalking = false;
            dialogue = null;
        }

        if (!isWriting && multiMessage)
        {
            if (dialogue.currentIndex >= dialogue.messages.Count - 1)
            {
                //check if there's a question
                if (dialogue.containsQuestion && !answered)
                {
                    StartCoroutine(AskOptions(dialogue.questionMessage, dialogue.option1, dialogue.option2));
                }
                else
                {
                    //we've reached the end, close
                    CloseDialogueBox();
                    dialogue.currentlyTalking = false;
                    dialogue = null;
                }
                
            }
            else
            {
                //still got more messages
                dialogue.currentIndex++;
                StartCoroutine(MakeText(dialogue.GetSpeech(dialogue.currentIndex)));
            }
        }
    }

    public void OpenDialogueBox()
    {
        textbox.SetActive(true);
    }

    public void CloseDialogueBox()
    {
        textbox.SetActive(false);
    }

    public void OpenOptionBox(string message, string option1, string option2)
    {
        option1text.text = option1;
        option2text.text = option2;
        SendText(message);
        optionBox.SetActive(true);
    }

    public IEnumerator AskOptions(string message, string option1, string option2)
    {
        OpenOptionBox(message,option1, option2);
        while(!answered)
        {
            yield return new WaitForEndOfFrame();
        }
        optionBox.SetActive(false);
        //ok they answered it

    }

    public void OptionPressed(int button)
    {
        if (button == 1)
        {
            //option 1
            SendText(dialogue.response1);
            answered = true;
        }
        else
        {
            //option 2
            SendText(dialogue.response2);
            answered = true;
        }
    }
}
