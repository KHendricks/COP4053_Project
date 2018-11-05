using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeObstacle : MonoBehaviour {

    public Dog dog;
    public DialogMessage dialog;
    bool crossing;

    void Start()
    {
        crossing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dog")
        {
            crossing = true;
            dog.followPlayer = false;
            dog.stateManager.Switch("stay");
            dialog.Activate("Boof boof! (That bridge looks sketchy...I'll wait for you here.)");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            crossing = false;
            dog.followPlayer = true;
            dog.stateManager.Switch("follow");
        }
    }

    void Update()
    {
        if(crossing)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                dialog.dismissed = true;
                dialog.Deactivate();
            }
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Dog" && !dialog.dismissed)
    //    {
    //        dog.followPlayer = false;
    //        dog.stateManager.Switch("stay");
    //        dialog.Activate("Boof boof! (That bridge looks sketchy...I'll wait for you here.)");

    //        dialog.dismissed |= Input.GetKeyDown(KeyCode.Z);

    //    }
    //    dialog.Deactivate();
    //}

}
