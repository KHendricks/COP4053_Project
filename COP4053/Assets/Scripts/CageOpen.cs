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

                // Replenishes the players heatlh
                // Can set an animation to play here
                PlayerPrefs.SetInt("PlayerHealth", 3);

                Destroy(this.gameObject);
            }

        }
    }
}
