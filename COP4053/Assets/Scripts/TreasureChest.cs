using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour {

    public ContextMessage context;
    public DialogMessage dialog;
    public Player player;
    public GameObject treasure;
    public string treasureName;
    protected bool openAttempted;

    private void Start()
    {
        openAttempted = false;
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !openAttempted)
        {
            context.Activate("open chest", context.a);

            if(Input.GetKeyDown(KeyCode.Z)){
                openAttempted = true;
                context.Deactivate(context.a);

                if (player.hasKey)
                {
                    // Play chest animation
                    // Present treasure
                    // Dialog about treasure
                    dialog.Activate("You got the " + treasureName + "!");
                    if(Input.GetKeyDown(KeyCode.Z))
                    {
                        dialog.Deactivate();
                    }
                    player.hasKey = false;
                    Destroy(this.gameObject);
                }
                else
                {
                    dialog.Activate("The chest is locked.");
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        dialog.Deactivate();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        openAttempted = false;
    }
}
