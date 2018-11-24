using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotAmmoRefill : MonoBehaviour 
{
    public DialogMessage dialog;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("SlingshotAmmo", 30);
            if (!dialog.active)
                dialog.Activate("You got some ammo!");
            Destroy(gameObject);
        }
    }
}
