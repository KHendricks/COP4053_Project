using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpen : MonoBehaviour {

    protected InputManager input;
    public Player player;
    public GameObject cage;
    public Dog dog;
    public ContextMessage context;
    public DialogMessage dialog;
    bool present;
    bool opened;
    bool debounce;

    void Start()
    {
        opened = false;
        debounce = false;
        present = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(dog.rescued == false && other.gameObject.tag == "Player")
        {
            present = true;
            context.Activate("(free dog)", context.a);
        }
    }

    void Update()
    {
        if(present)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                dog.followPlayer = true;
                context.Deactivate(context.a);

                PlayerPrefs.SetInt("PlayerHealth", 3);

                dialog.Activate("There was something else in the cage... you found a key!");
                player.hasKey = true;
                Destroy(cage);
            }
        }
    }

    //void OnTriggerStay(Collider other) {
    //    if(dog.rescued == false && other.gameObject.tag == "Player") 
    //    {
    //        context.Activate("free dog", context.a);

    //        opened |= Input.GetKeyDown(KeyCode.Z);



    //        if (opened && !dialog.dismissed)
    //        {
    //            debounce |= Input.GetKeyUp(KeyCode.Z);
    //            Debug.Log("cage debounce = " + debounce);
    //            dog.followPlayer = true;
    //            context.Deactivate(context.a);

    //            // Replenishes the players heatlh
    //            // Can set an animation to play here
    //            PlayerPrefs.SetInt("PlayerHealth", 3);

    //            dialog.Activate("There was something else in the cage... you found a key!");
    //            player.hasKey = true;

    //            if(debounce)
    //            {
    //                dialog.dismissed |= Input.GetKeyDown(KeyCode.Z);
    //                Debug.Log("cage message dismissed = " + dialog.dismissed);
    //            }
    //            Destroy(this.gameObject);
    //        }

    //    }
    //    dialog.Deactivate();
    //}

    private void OnTriggerExit(Collider other)
    {
        context.Deactivate(context.a);
        present = false;
        dialog.dismissed = true;
        dialog.Deactivate();
    }
}
