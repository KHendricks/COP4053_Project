﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{
    public float speed = 3f;
    public float sprintMultiplier = 3f;
    public float jumpHeight = 3f;
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
        CheckInput();
	}

    void CheckInput()
    {
        if (Input.GetKeyDown("space"))
            Jump();
        if (Input.GetKeyDown("left shift"))
            Sprint(true);
        if (Input.GetKeyUp("left shift"))
            Sprint(false);
    }

    void Jump()
    {
        // jumpHeight is currently a magic number
        Vector3 dirVector = new Vector3(0, jumpHeight, 0).normalized;
        GetComponent<Rigidbody>().AddRelativeForce(transform.position + dirVector * jumpHeight);
    }

    void Movement()
    {
        Vector3 dirVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        GetComponent<Rigidbody>().MovePosition(transform.position + dirVector * Time.deltaTime * speed);
    }

    void Sprint(bool isSprinting)
    {
        // Player holds down the key to move fatser. Sprinting ends when player stops holding the key down
        if (isSprinting)
            speed *= sprintMultiplier;
        else
            speed /= sprintMultiplier;
    }
}
