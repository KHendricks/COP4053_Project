using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMessage : MonoBehaviour {

    public Text message;
    public GameObject messageContainer;
    public ContextMessage context;
    public bool active;

	// Use this for initialization
	void Start () {
        messageContainer.SetActive(false);
        active = false;
    }

    void Update()
    {
        if (active && InputManager.JustPressed(InputAction.Dismiss))
            Deactivate();
    }

    public void Activate(string message)
    {
        active = true;
        messageContainer.SetActive(true);
        Debug.Log("Trying to activate " + message + " dialog");
        this.message.text = message;
        context.Activate("(dismiss)", InputAction.Dismiss);
    }

    public void Deactivate()
    {
        context.Deactivate();
        messageContainer.SetActive(false);
        Debug.Log("Deactivating " + message + " dialog");
        message.text = "";
        active = false;
    }

}
