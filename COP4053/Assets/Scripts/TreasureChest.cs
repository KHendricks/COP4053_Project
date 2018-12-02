using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour {

    public GameObject chest;
    public ContextMessage context;
    public DialogMessage dialog;
    public Player player;
    public string treasureName;
    bool present;
    bool opened;

    private void Start()
    {
        present = false;
        opened = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            present = true;
            context.Activate("(open chest)", InputAction.Interact);
        }
    }

    void Update()
    {
        if (present)
        {
            if(InputManager.JustPressed(InputAction.Interact))
            {
                context.Deactivate();

                if(player.hasKey)
                {
                    opened = true;
                    PlayerPrefs.SetInt(treasureName, 1);
                    if (!dialog.active)
                        dialog.Activate("You got the " + treasureName + "! Press Shift key or B button to use.");
                    player.hasKey = false;
                    Destroy(chest);
                }
                else
                {
                    if (!dialog.active)
                        dialog.Activate("It's locked.");
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        present = false;
        context.Deactivate();

        if(opened)
            Destroy(this);
    }
}
