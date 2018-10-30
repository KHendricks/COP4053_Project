using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeObstacle : MonoBehaviour {

    public Dog dog;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Dog")
        {
            dog.followPlayer = false;
            dog.stateManager.Switch("stay");
        }
    }
}
