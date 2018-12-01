using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpen : MonoBehaviour {

    public Player player;
    public GameObject cage;
    public Dog dog;
    public ContextMessage context;
    public DialogMessage dialog;
    bool present;
    public bool giveKey;

    void Start()
    {
        present = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(dog.rescued == false && other.gameObject.tag == "Player")
        {
            present = true;
            context.Activate("(free dog)", InputAction.Interact);
        }
    }

    void Update()
    {
        if (present)
        {
            if(InputManager.JustPressed(InputAction.Interact))
            {
                dog.rescued = true;
                dog.followPlayer = true;
                player.dogs.Add(dog);
                context.Deactivate();

                //PlayerPrefs.SetInt("PlayerHealth", 3);
                if(giveKey)
                {
                    if (!dialog.active)
                        dialog.Activate("There was something else in the cage... you found a key!");
                    player.hasKey = true;
                    Destroy(cage);
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        context.Deactivate();
        present = false;

        if(dog.rescued)
            Destroy(this);
    }
}
