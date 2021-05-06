using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public static MessageBox instance;

    public GameObject container;

    public Coroutine closeBox;

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
        setText(text, 2f);
    }

    public void setText(string text, float duration)
    {
        container.SetActive(true);
        messageText.text = text;
        if (debug) Debug.Log(text);
        if (closeBox != null) StopCoroutine(closeBox);
        closeBox = StartCoroutine(CloseMessageBox(duration));
    }

    public IEnumerator CloseMessageBox(float duration)
    {
        yield return new WaitForSeconds(duration);
        container.SetActive(false);
    }
    
}
