using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePickup : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Knife", 1);
            Destroy(gameObject);
        }
    }
}
