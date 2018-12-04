using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossArea : MonoBehaviour 
{

    public GameObject boss;
    private bool engaged;
    public DialogMessage dialog1;
    public DialogMessage dialog2;
    public DialogMessage dialog3;
    public DialogMessage dialog4;
    bool done1, exists1;
    bool done2, exists2;
    bool done3, exists3;
    bool done4, exists4;
    Player player;


    // Use this for initialization
    void Start () 
    {
        engaged = false;
    }
	
	// Update is called once per frame
	void Update () 
    {
        // Keeps the collision area attached to boss' position
        gameObject.transform.position = boss.transform.position;
	}

    public bool GetStatus()
    {
        return engaged;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Freeze player until dialog is complete.
            player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                if (done1 && done2 && done3 && done4)
                {
                    engaged = true;
                    player.freeze = false;
                }
                else
                {
                    player.freeze = true;
                    if (!done1)
                    {
                        if (!exists1)
                        {
                            dialog1.Activate("Haw haw! I knew someday a self-righteous wannabe-hero would come to challenge me.");
                            exists1 = true;
                        }
                        else if (!dialog1.active)
                            done1 = true;
                    }
                    else if (!done2)
                    {
                        if (!exists2)
                        {
                            dialog2.Activate("Oh she's yours, eh? Well if you want that glorious pelt up there, you'll have to go through me.");
                            exists2 = true;
                        }
                        else if (!dialog2.active)
                            done2 = true;
                    }
                    else if (!done3)
                    {
                        if (!exists3)
                        {
                            dialog3.Activate("You have no idea the price I could fetch for the fur of that slobbering beast...");
                            exists3 = true;
                        }
                        else if (!dialog3.active)
                            done3 = true;
                    }
                    else if (!done4)
                    {
                        if (!exists4)
                        {
                            dialog4.Activate("I would never surrender such treasure to the likes of you!");
                            exists4 = true;
                        }
                        else if (!dialog4.active)
                            done4 = true;
                    }
                }
            }
            else
                Debug.Log("Player is null");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            engaged = false;
        }
    }
}
