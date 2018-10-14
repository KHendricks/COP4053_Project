using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : Movement 
{
    public LayerMask groundLayer;
    public float sprintMultiplier = 3f;
    public float jumpHeight = 5f;
    public bool attack;
    public PlayerState state;
    Animator animator;


	// Use this for initialization
	void Start () 
    {
        animator = GetComponentInChildren<Animator>();
        state = PlayerState.NORMAL;
    }
	
	// Update is called once per frame
	void Update () 
    {
        switch (state) {
            case PlayerState.NORMAL:
                if (!IsGrounded())
                {
                    state = PlayerState.JUMP;
                    Animate("Jump");
                    break;
                }
                Movement();
                CheckInput();
                if (isStationary)
                    Animate("Idle");
                else
                    Animate("Walk");
                break;
            case PlayerState.JUMP:
                if(IsGrounded()) {
                    state = PlayerState.NORMAL;
                    break;
                }
                Movement();
                break;
            case PlayerState.ATTACK:
                Animate("Attack");
                break;
        }
	}

    void CheckInput()
    {
        //Debug.Log(IsGrounded());
        // JoystickButton0 is the "A" button on an Xbox one controller
        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.JoystickButton0))
            Jump();
        // JoystickButton8 is the left thumbstick on an Xbox one controller
        if (Input.GetKeyDown("left shift") || Input.GetKeyDown(KeyCode.JoystickButton8))
            Sprint(true);
        if (Input.GetKeyUp("left shift") || Input.GetKeyUp(KeyCode.JoystickButton8))
            Sprint(false);
        if (Input.GetKeyDown(KeyCode.B))
            Attack(true);
        if (Input.GetKeyUp(KeyCode.B))
            Attack(false);
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

    void Attack(bool isAttacking)
    {
        if (isAttacking)
            attack = true;
        else
            attack = false;
    }

    void Sprint(bool isSprinting)
    {
        // Player holds down the key to move fatser. Sprinting ends when player stops holding the key down
        if (isSprinting)
            speed *= sprintMultiplier;
        else
            speed /= sprintMultiplier;
    }

    void Animate(string action)
    {
        animator.SetFloat("FaceZ", currentDirection.y);
        animator.SetFloat("FaceX", currentDirection.x);

        if (!IsGrounded())
            animator.Play("Jump");
        if (attack)
            animator.Play("Attack");
        else if (isStationary)
            animator.Play("Idle");
        else
            animator.Play("Walk");
    }
}

public enum PlayerState
{
    NORMAL,
    JUMP,
    ATTACK
}