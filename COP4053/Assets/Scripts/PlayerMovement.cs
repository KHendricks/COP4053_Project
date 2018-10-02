using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Movement 
{
    public LayerMask groundLayer;
    public float sprintMultiplier = 3f;
    public float jumpHeight = 5f;
    Animator animator;


	// Use this for initialization
	void Start () 
    {
        animator = GetComponentInChildren<Animator>();
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
        //Debug.Log(IsGrounded());
        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.JoystickButton2))
            Jump();
        if (Input.GetKeyDown("left shift") || Input.GetKeyDown(KeyCode.JoystickButton3))
            Sprint(true);
        if (Input.GetKeyUp("left shift") || Input.GetKeyDown(KeyCode.JoystickButton3))
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

        PlayerPrefs.SetFloat("PlayerDirectionX", dir.x);
        PlayerPrefs.SetFloat("PlayerDirectionY", dir.y);
        PlayerPrefs.SetFloat("PlayerDirectionZ", dir.z);

        SetFacing(dir);
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
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

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
        animator.SetFloat("FaceZ", currentDirection.y);
        animator.SetFloat("FaceX", currentDirection.x);

        if (!IsGrounded())
            animator.Play("Jump");
        else if (isStationary)
            animator.Play("Idle");
        else
            animator.Play("Walk");
    }
}
