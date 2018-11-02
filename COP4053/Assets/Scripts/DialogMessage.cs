using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMessage : MonoBehaviour {

    public Text message;
    public GameObject messageContainer;
    public bool dismissed;

	// Use this for initialization
	void Start () {
        messageContainer.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Activate(string message)
    {
        messageContainer.SetActive(true);
        this.message.text = message;
    }

    public void Deactivate()
    {
        messageContainer.SetActive(false);
        message.text = "";
    }

}
