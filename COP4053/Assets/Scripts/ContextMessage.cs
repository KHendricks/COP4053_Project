using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMessage : MonoBehaviour
{
    public Text actionOption;
    public GameObject messageContainer;
    public GameObject b;
    public GameObject x;
    public GameObject y;
    public GameObject back;
    public GameObject xKey;
    public GameObject zKey;
    public GameObject shiftKey;
    public GameObject cKey;

    GameObject interact;
    GameObject dismiss;
    GameObject attack;
    GameObject followToggle;

    GameObject active;
    public bool shown;
    public bool incoming;


    void Start()
    {
        messageContainer.SetActive(false);
        b.SetActive(false);
        x.SetActive(false);
        y.SetActive(false);
        back.SetActive(false);
        xKey.SetActive(false);
        zKey.SetActive(false);
        shiftKey.SetActive(false);
        cKey.SetActive(false);
    }

    public void Activate(string message, InputAction action)
    {
        incoming = true;
        if (shown)
        {
            Debug.Log("a context is already shown for" + message);
            Deactivate();
        }
        shown = true;
        messageContainer.SetActive(true);
        actionOption.text = message;

        if(InputManager.isGamepad)
        {
            interact = x;
            attack = b;
            followToggle = y;
            dismiss = back;
        }
        else
        {
            interact = xKey;
            attack = shiftKey;
            followToggle = zKey;
            dismiss = cKey;
        }

        switch (action)
        {
            case InputAction.Interact:
                interact.SetActive(true);
                active = interact;
                break;
            case InputAction.Attack:
                attack.SetActive(true);
                active = attack;
                break;
            case InputAction.FollowToggle:
                followToggle.SetActive(true);
                active = followToggle;
                break;
            case InputAction.Dismiss:
                dismiss.SetActive(true);
                active = dismiss;
                break;
            default:
                return;

        }
        incoming = false;
    }

    public void Deactivate()
    {
        shown = false;
        messageContainer.SetActive(false);
        actionOption.text = "";
        active.SetActive(false);
    }

}
