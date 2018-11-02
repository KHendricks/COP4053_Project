using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeObstacle : MonoBehaviour {

    public Dog dog;
    public DialogMessage dialog;


    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Dog" && !dialog.dismissed)
        {
            dog.followPlayer = false;
            dog.stateManager.Switch("stay");
            dialog.Activate("Boof boof! /n(That bridge looks sketchy...I'll wait for you here.)");

            if(Input.GetKeyDown(KeyCode.Z))
            {
                dialog.Deactivate();
                dialog.dismissed = true;
            }
        }
    }

}
