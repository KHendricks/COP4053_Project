using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpen : MonoBehaviour {

    public Dog dog;
    public ContextMessage context;

    void OnTriggerStay(Collider other) {
        if(dog.rescued == false && other.gameObject.tag == "Player") 
        {
            context.Activate("free dog", context.a);

            if(Input.GetKeyDown(KeyCode.Z))
            {
                dog.followPlayer = true;
                context.Deactivate(context.a);
                Destroy(this.gameObject);
            }

        }
    }
}
