using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{
    public LayerMask groundLayer;
    public float speed = 3f;
    public float sprintMultiplier = 3f;
    public float jumpHeight = 5f;
    Rigidbody rb;
    Animator animator;


	// Use this for initialization
	void Start () 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        Movement();
        CheckInput();
	}

    void CheckInput()
    {
        Debug.Log(IsGrounded());
        if (Input.GetKeyDown("space"))
            Jump();
        if (Input.GetKeyDown("left shift"))
            Sprint(true);
        if (Input.GetKeyUp("left shift"))
            Sprint(false);
    }

    private bool IsGrounded()
    {
        float distanceToGround = .2f;

        Vector3 playerPosition = gameObject.transform.position;
        Debug.DrawRay(playerPosition, Vector3.down, Color.red);
        bool isGrounded = Physics.Raycast(playerPosition, Vector3.down, distanceToGround, groundLayer);

        return isGrounded;
    }

    void Jump()
    {
        // jumpHeight is currently a magic number
        if (IsGrounded())
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    void Movement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        var camera = Camera.main;

        // Camera's forward and right vectors
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        // Project forward and right vectors on horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Direction we want to move
        var dir = forward * verticalAxis + right * horizontalAxis;

        animator.SetFloat("FaceX", dir.z);
        animator.SetFloat("FaceY", -dir.x);


        transform.Translate(dir * speed * Time.deltaTime);
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
