using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{
    public float speed = 3f;
    Rigidbody rb;

	// Use this for initialization
	void Start () 
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () 
    {
        Movement();
	}

    void Movement()
    {
        Vector3 dirVector = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")).normalized;
        GetComponent<Rigidbody>().MovePosition(transform.position + dirVector * Time.deltaTime);
    }
}
