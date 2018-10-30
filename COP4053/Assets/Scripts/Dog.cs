using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dog : Movement 
{

    public Animator animator;
    public StateManager<Dog> stateManager;
    public bool rescued;
    public bool followPlayer;
    public Player player;
    public float distanceFromPlayer;

	// Use this for initialization
	void Start () 
    {
        rescued = false;
        animator = GetComponentInChildren<Animator>();
        stateManager = new StateManager<Dog>();
        //stateManager.Add("caged", new CagedState());
        stateManager.Add("stay", new StayState());
        stateManager.Add("follow", new FollowState());
        stateManager.Switch("stay");
    }

    public void Follow()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Move our position a step closer to the target, if player is far enough away
        if (distance > distanceFromPlayer)
        {
            // Lerp instead of MoveTowards to smooth out the transition.
            //var dir = Vector3.MoveTowards(transform.position, player.transform.position, step);
            var dir = Vector3.Lerp(transform.position, player.transform.position, step);
            transform.position = dir;

            Vector3 newDir = new Vector3(PlayerPrefs.GetFloat("PlayerDirectionX"), PlayerPrefs.GetFloat("PlayerDirectionY"), PlayerPrefs.GetFloat("PlayerDirectionZ"));
            SetFacing(newDir);
            animator.SetFloat("FaceZ", currentDirection.y);
            animator.SetFloat("FaceX", currentDirection.x);
        }
    }

    // Update is called once per frame
    void Update () 
    {
        stateManager.Update(this);
	}
}
