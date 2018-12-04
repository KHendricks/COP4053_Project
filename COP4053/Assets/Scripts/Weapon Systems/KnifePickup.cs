using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePickup : MonoBehaviour 
{
    public DialogMessage dialog;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Knife", 1);
            if (!dialog.active)
                dialog.Activate("You got the knife!");
            Destroy(gameObject);
        }
    }
}
