using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotAmmoRefill : MonoBehaviour 
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("SlingshotAmmo", 30);
            Destroy(gameObject);
        }
    }
}
