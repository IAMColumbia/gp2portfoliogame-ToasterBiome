using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public static MessageBox instance;

    public TextMeshProUGUI messageText;
    public bool debug = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("A MessageBox already exists");
            Destroy(gameObject);
        }
    }
    
    public void setText(string text)
    {
        messageText.text = text;
        if (debug) Debug.Log(text);
    }
    
}
