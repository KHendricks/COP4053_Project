using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : IState<Dog> {

	// Use this for initialization
    public void OnEnter(Dog owner) 
    {
        GameObject.FindObjectOfType<AudioManager>().Play("Woof");
    }
	
	// Update is called once per frame
	public void Update (Dog owner) 
    {
        if (!owner.followPlayer)
        {
            owner.stateManager.Switch("stay");
        }
        else
        {
            owner.Follow();
            if (owner.player.isStationary)
                owner.animator.Play("Idle");
            else
                owner.animator.Play("Trot");
        }


	}

    public void OnExit(Dog owner) 
    {
        // Run away to freedom or something
    }
}
