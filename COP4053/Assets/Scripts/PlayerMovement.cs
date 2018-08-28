using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float speed = 3f;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        Movement();
	}

    void Movement()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, z) * speed * Time.deltaTime;
        gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + movement);
    }
}
