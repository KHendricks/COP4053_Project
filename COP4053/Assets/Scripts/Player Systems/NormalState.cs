using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : IState<Player> {

    public void OnEnter(Player owner)
    {

    }

    // Update is called once per frame
    public void Update (Player owner) 
    {
        owner.Movement();
        if (!owner.IsGrounded()) {
            owner.stateManager.Switch("freefall");
            Object.FindObjectOfType<AudioManager>().Stop("Footsteps");
        }
        if (owner.isStationary)
        {
            owner.animator.Play("Idle");
            Object.FindObjectOfType<AudioManager>().Play("Footsteps");
        }
        else
        {
            owner.animator.Play("Walk");
        }

        // Jump
        if (InputManager.JustPressed(InputAction.Jump))
        {
            if (owner.IsGrounded())
            {
                owner.GetComponent<Rigidbody>().AddForce(Vector3.up * owner.jumpHeight, ForceMode.Impulse);
                Object.FindObjectOfType<AudioManager>().Play("Jump");
            }
        }
        // Attack
        if (InputManager.JustPressed(InputAction.Attack))
        {
            owner.stateManager.Switch("attack");
        }

        // Make dog stay/come
        if(InputManager.JustPressed(InputAction.FollowToggle))
        {
            foreach(Dog dog in owner.dogs)
            {
                if(dog.closeEnough)
                {
                    dog.followPlayer = !dog.followPlayer;
                }
            }
        }
    }

    public void OnExit(Player owner)
    {

    }
}
