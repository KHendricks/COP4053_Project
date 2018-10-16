using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpen : MonoBehaviour {

    public Dog dog;

    void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Z)) {
            dog.rescued = true;
        }
    }
}
