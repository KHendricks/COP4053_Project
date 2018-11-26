using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Lasso" && other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
