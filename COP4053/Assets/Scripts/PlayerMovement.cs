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

        Vector2 playerPosition = gameObject.transform.position;
        Debug.DrawRay(playerPosition, Vector2.down, Color.red);
        bool isGrounded = Physics.Raycast(playerPosition, Vector2.down, distanceToGround, groundLayer);

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

        GetAngleIndex(forward);

        // Direction we want to move;
        var dir = forward * verticalAxis + right * horizontalAxis;

        transform.Translate(dir * speed * Time.deltaTime);
        //Vector3 dirVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        //GetComponent<Rigidbody>().MovePosition(transform.position + dirVector * Time.deltaTime * speed);
    }

    void Sprint(bool isSprinting)
    {
        // Player holds down the key to move fatser. Sprinting ends when player stops holding the key down
        if (isSprinting)
            speed *= sprintMultiplier;
        else
            speed /= sprintMultiplier;
    }

    int GetAngleIndex(Vector3 forward)
    {
        var dir = Camera.main.transform.position - forward;
        var playerAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (playerAngle < 0.0f)
            playerAngle += 360;
        Debug.Log("Angle from the player is: " + playerAngle);
        if (playerAngle >= 292.5f && playerAngle < 337.5f)
            return 8;
        else if (playerAngle >= 22.5f && playerAngle < 67.5f)
            return 2;
        else if (playerAngle >= 67.5f && playerAngle < 112.5f)
            return 3;
        else if (playerAngle >= 112.5f && playerAngle < 157.5f)
            return 4;
        else if (playerAngle >= 157.5f && playerAngle < 202.5f)
            return 5;
        else if (playerAngle >= 202.5f && playerAngle < 247.5f)
            return 6;
        else if (playerAngle >= 247.5f && playerAngle < 292.5f)
            return 7;
        else if (playerAngle >= 337.5f || playerAngle < 22.5f)
            return 1;
        else return 0;
    }
}
