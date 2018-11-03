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
    bool openAttempted;
    bool interacted;
    bool present;

    private void Start()
    {
        openAttempted = false;
        interacted = false;
        present = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            present = true;
            context.Activate("(open chest)", context.a);
        }
    }

    void Update()
    {
        if(present)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                context.Deactivate(context.a);

                if(player.hasKey)
                {
                    PlayerPrefs.SetInt(treasureName, 1);
                    Debug.Log("should be getting slingshot");
                    dialog.Activate("You got the " + treasureName + "!");
                    Debug.Log("did it show message?");
                    player.hasKey = false;
                    Destroy(chest);
                }
                else
                {
                    dialog.Activate("It's locked.");
                }
            }
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Player" && !openAttempted)
    //    {
    //        context.Activate("open chest", context.a);
    //        interacted |= Input.GetKeyDown(KeyCode.Z);
    //        if(interacted)
    //        {
    //            openAttempted = true;
    //            context.Deactivate(context.a);

    //            if (player.hasKey)
    //            {
    //                // Play chest animation
    //                // Present treasure
    //                // Dialog about treasure
    //                dialog.Activate("You got the " + treasureName + "!");
    //                Debug.Log("treasure message dismissed = " + dialog.dismissed);
    //                player.hasKey = false;
    //                Destroy(chest);
    //            }
    //            else
    //            {
    //                dialog.Activate("The chest is locked.");
    //            }
    //        }
    //        dialog.dismissed |= Input.GetKeyDown(KeyCode.Z);
    //        dialog.Deactivate();
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        present = false;
        openAttempted = false;
        context.Deactivate(context.a);
        dialog.dismissed = true;
        dialog.Deactivate();
    }
}
