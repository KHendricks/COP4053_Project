using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : IState<Dog> {

	// Use this for initialization
    public void OnEnter(Dog owner) 
    {
        // present player with item or health
    }
	
	// Update is called once per frame
	public void Update (Dog owner) 
    {
        owner.Follow();
        if (owner.isStationary)
            owner.animator.Play("Idle");
        else
            owner.animator.Play("Trot");

	}

    public void OnExit(Dog owner) 
    {
        // Run away to freedom or something
    }
}
