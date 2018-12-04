using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotPickup : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Slingshot", 1);
            Destroy(gameObject);
        }
    }
}
