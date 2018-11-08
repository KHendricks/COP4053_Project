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
            if (!dialog.active)
                dialog.Activate("Boof boof! (That bridge looks sketchy...I'll wait for you here.)");
        }
    }

    //void Update()
    //{
    //    if(dialog.active && crossing)
    //    {
    //        if(InputManager.JustPressed(InputAction.Interact))
    //        {
    //            dialog.Deactivate();
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        crossing = false;
    }
}
