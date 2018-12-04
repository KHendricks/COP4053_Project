using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour 
{

    public GameObject boss;
    private bool engaged;

	// Use this for initialization
	void Start () 
    {
        engaged = false;	
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Keeps the collision area attached to boss' position
        gameObject.transform.position = boss.transform.position;
	}

    public bool GetStatus()
    {
        return engaged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            engaged = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            engaged = false;
        }
    }
}
