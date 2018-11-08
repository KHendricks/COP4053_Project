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
        }
        if (owner.isStationary)
            owner.animator.Play("Idle");
        else
            owner.animator.Play("Walk");

        // Jump
        if (InputManager.JustPressed(InputAction.Jump))
        {
            if (owner.IsGrounded())
                owner.GetComponent<Rigidbody>().AddForce(Vector3.up * owner.jumpHeight, ForceMode.Impulse);

        }
        // Attack
        if (InputManager.JustPressed(InputAction.Attack))
            owner.stateManager.Switch("attack");

        // Make dog stay/come
        if(InputManager.JustPressed(InputAction.FollowToggle))
        {
            if (owner.dog.followPlayer)
                owner.dog.followPlayer = false;
            else
            {
                //float distance = Vector3.Distance(owner.transform.position, owner.dog.transform.position);
                //if (distance <= owner.dog.distanceFromPlayer)
                    //owner.dog.followPlayer = true;
                if(owner.dog.closeEnough)
                {
                    owner.dog.followPlayer = true;
                }
            }

        }
    }

    public void OnExit(Player owner)
    {

    }
}
