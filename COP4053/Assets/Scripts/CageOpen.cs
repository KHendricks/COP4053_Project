using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpen : MonoBehaviour {

    public Player player;
    public Dog dog;
    public ContextMessage context;
    public DialogMessage dialog;

    void OnTriggerStay(Collider other) {
        if(dog.rescued == false && other.gameObject.tag == "Player") 
        {
            context.Activate("free dog", context.a);

            if(Input.GetKeyDown(KeyCode.Z) && !dialog.dismissed)
            {
                dog.followPlayer = true;
                context.Deactivate(context.a);

                // Replenishes the players heatlh
                // Can set an animation to play here
                PlayerPrefs.SetInt("PlayerHealth", 3);

                dialog.Activate("There was something else in the cage... you found a key!");
                player.hasKey = true;
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    dialog.Deactivate();
                    dialog.dismissed = true;
                }


                Destroy(this.gameObject);
            }

        }
    }

}
