using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour 
{
    private bool enableAttack, mutex;

	// Use this for initialization
	void Start () 
    {
        enableAttack = mutex = true;

        // Disable the collider at the start
        gameObject.GetComponent<Collider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        KnifeAttack();	
	}

    void KnifeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Return) && enableAttack)
        {
            if (mutex)
            {
                mutex = false;
                StartCoroutine("EnableAttack");
            }
        }
    }

    IEnumerator EnableAttack()
    {
        enableAttack = false;
        gameObject.GetComponent<Collider>().enabled = true;

        // Attack animation here

        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<Collider>().enabled = false;
        enableAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Disable the collider on enter to prevent multiple "attacks"
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
