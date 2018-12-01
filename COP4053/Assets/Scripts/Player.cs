using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Movement {

    public LayerMask groundLayer;
    public float sprintMultiplier = 3f;
    public float jumpHeight = 5f;
    public Animator animator;
    public StateManager<Player> stateManager;
    public GameObject knife;
    public bool showKnife;
    public List<Dog> dogs;
    public bool hasKey;

    private bool flashTimer;
    public SpriteRenderer playerSpriteRenderer;

	// Use this for initialization
	void Start () {
        dogs = new List<Dog>();
        flashTimer = true;
        showKnife = false;
        hasKey = false;
        animator = GetComponentInChildren<Animator>();
        stateManager = new StateManager<Player>();
        stateManager.Add("normal", new NormalState());
        stateManager.Add("freefall", new FreefallState());
        stateManager.Add("attack", new AttackState());
        // Add other states here.

        // Switch to initial state.
        stateManager.Switch("normal");

	}

    // Update is called once per frame
    void Update()
    {
        stateManager.Update(this);
    }

    public void Movement()
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

        animator.SetFloat("FaceZ", currentDirection.y);
        animator.SetFloat("FaceX", currentDirection.x);
    }

    public bool IsGrounded()
    {
        float distanceToGround = .2f;

        Vector3 playerPosition = gameObject.transform.position;
        Debug.DrawRay(playerPosition, Vector3.down, Color.red);
        bool isGrounded = Physics.Raycast(playerPosition, Vector3.down, distanceToGround, groundLayer);

        return isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BossRock")
        {
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("PlayerHealth", PlayerPrefs.GetInt("PlayerHealth") - 1);
            if (flashTimer)
            {
                flashTimer = false;
                StartCoroutine(Flash(playerSpriteRenderer.material));
            }
        }
    }

    // The variable being passed is the spriteRenderer material of the object
    // that needs to "Flash"
    IEnumerator Flash(Material material)
    {
        material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.33f);
        material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(0.33f);

        flashTimer = true;
    }
}
