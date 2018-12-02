using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dog : Movement 
{
    public LayerMask groundLayer;
    public float jumpHeight = 4f;
    public Animator animator;
    public StateManager<Dog> stateManager;
    public bool rescued;
    public bool followPlayer;
    public Player player;
    public float distanceFromPlayer;
    public ContextMessage context;
    public bool closeEnough;
    public float maxHeightDiff;

	// Use this for initialization
	void Start () 
    {
        closeEnough = false;
        followPlayer = false;
        rescued = false;
        animator = GetComponentInChildren<Animator>();
        stateManager = new StateManager<Dog>();
        stateManager.Add("caged", new CagedState());
        stateManager.Add("rescued", new FollowState());
        stateManager.Add("stay", new StayState());
        stateManager.Add("follow", new FollowState());
        stateManager.Switch("caged");
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Player" && rescued && !context.shown)
    //    {
    //        closeEnough = true;
    //        if (player.dogsFollow)
    //            context.Activate("stay", InputAction.FollowToggle);
    //        else
    //            context.Activate("come", InputAction.FollowToggle);
    //    }
    //}

    public void Follow()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.fixedDeltaTime;
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Move our position a step closer to the target, if player is far enough away
        if (distance > distanceFromPlayer && !player.isStationary)
        {
            // Lerp to smooth out the transition.
            var dir = Vector3.MoveTowards(transform.position, player.transform.position, step);
            transform.position = dir;

            // Jumping
            //var heightDiff = player.transform.position.y - transform.position.y;
            //Debug.Log(heightDiff);
            //if (heightDiff > maxHeightDiff && IsGrounded()){
            //    GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            //}
        }

        NormalizeDirection(player.transform.position, transform.position);
        animator.SetFloat("FaceZ", currentDirection.y);
        animator.SetFloat("FaceX", currentDirection.x);

    }

    bool ObstacleAhead()
    {
        float distancefromObstacle = .2f;
        Vector3 dogPosition = gameObject.transform.position;
        Debug.DrawRay(dogPosition, currentDirection, Color.red);
        return Physics.Raycast(dogPosition, currentDirection, distancefromObstacle, groundLayer);
    }

    bool IsGrounded()
    {
        float distanceToGround = .1f;

        Vector3 dogPosition = gameObject.transform.position;
        Debug.DrawRay(dogPosition, Vector3.down, Color.red);
        return Physics.Raycast(dogPosition, Vector3.down, distanceToGround, groundLayer);
    }

    // Update is called once per frame
    void Update () 
    {
        stateManager.Update(this);
	}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        closeEnough = false;
    //        context.Deactivate();
    //    }
            
    //}
}
