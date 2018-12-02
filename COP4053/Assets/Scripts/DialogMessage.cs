using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMessage : MonoBehaviour {

    public Text message;
    public GameObject messageContainer;
    public GameObject c;
    public GameObject back;
    public bool active;

    GameObject dismiss;

	// Use this for initialization
	void Start () {
        messageContainer.SetActive(false);
        c.SetActive(false);
        back.SetActive(false);
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
        dismiss = InputManager.isGamepad ? back : c;
        dismiss.SetActive(true);
        Debug.Log("Trying to activate " + message + " dialog");
        this.message.text = message;
        StartCoroutine(MessageTimer());

    }

    public void Deactivate()
    {
        messageContainer.SetActive(false);
        dismiss.SetActive(false);
        Debug.Log("Deactivating " + message + " dialog");
        message.text = "";
        active = false;
    }

    IEnumerator MessageTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        Deactivate();
    }

}
