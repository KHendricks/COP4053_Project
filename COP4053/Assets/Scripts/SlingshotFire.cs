using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotFire : MonoBehaviour 
{
    // Have the rock prefab loaded into this variable
    // through the editor
    public GameObject slingshotRockPrefab;
    public float rockSpeed = 3f;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        FireSlingshot();
	}

    public void FireSlingshot()
    {
        // On enter press instantiate a rock projectile
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject rockProjectile = Instantiate(slingshotRockPrefab, gameObject.transform.position, Quaternion.identity);
            rockProjectile.GetComponent<Rigidbody>().velocity = transform.forward * rockSpeed;
            Destroy(rockProjectile, 5);
        }
    }
}
