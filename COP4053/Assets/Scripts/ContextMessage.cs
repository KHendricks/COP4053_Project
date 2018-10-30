using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMessage : MonoBehaviour
{
    public Text actionOption;
    public GameObject messageContainer;
    public GameObject a;
    public GameObject b;
    public GameObject x;
    public GameObject y;
    public GameObject z;

    void Start()
    {
        //Text sets your text to say this message
        //actionOption.text = "jump";
        messageContainer.SetActive(false);
        a.SetActive(false);
        b.SetActive(false);
        x.SetActive(false);
        y.SetActive(false);
        z.SetActive(false);
    }

    void Update()
    {

    }

    public void Activate(string message, GameObject button)
    {
        messageContainer.SetActive(true);
        actionOption.text = message;
        button.SetActive(true);
    }

    public void Deactivate(GameObject button)
    {
        messageContainer.SetActive(false);
        actionOption.text = "";
        button.SetActive(false);
    }

}
