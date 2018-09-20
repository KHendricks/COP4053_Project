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
    public float currZ;
    public float currX;
    public bool isStationary;


	// Use this for initialization
	void Start () 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        currZ = 0;
        currX = 0;
        isStationary = true;
    }
	
	// Update is called once per frame
	void Update () 
    {
        Movement();
        CheckInput();
        Animate();
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
        transform.Translate(dir * speed * Time.deltaTime);

        SetFacing(dir);
    }

    // Determine what direction the character is facing and whether they
    // are moving.
    void SetFacing(Vector3 dir)
    {
        if (!Mathf.Approximately(dir.z, 0f) && !Mathf.Approximately(dir.x, 0f))
        {
            currZ = dir.z;
            currX = -dir.x;
            isStationary = false;
        }
        else
            isStationary = true;
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

    void Sprint(bool isSprinting)
    {
        // Player holds down the key to move fatser. Sprinting ends when player stops holding the key down
        if (isSprinting)
            speed *= sprintMultiplier;
        else
            speed /= sprintMultiplier;
    }

    void Animate()
    {
        animator.SetFloat("FaceZ", currZ);
        animator.SetFloat("FaceX", currX);

        if (!IsGrounded())
            animator.Play("Jump");
        else if (isStationary)
            animator.Play("Idle");
        else
            animator.Play("Walk");
    }
}
