using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoPickup : MonoBehaviour 
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Lasso", 1);
            Destroy(gameObject);
        }
    }
}
