using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoPickup : MonoBehaviour 
{
    public DialogMessage dialog;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Lasso", 1);
            if (!dialog.active)
                dialog.Activate("You got the lasso!");
            Destroy(gameObject);
        }
    }
}
