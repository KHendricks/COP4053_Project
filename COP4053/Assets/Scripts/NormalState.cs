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
        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.JoystickButton0)) 
        {
            if (owner.IsGrounded())
                owner.GetComponent<Rigidbody>().AddForce(Vector3.up * owner.jumpHeight, ForceMode.Impulse);

        }
        // Attack
        if (Input.GetKeyDown(KeyCode.B))
            owner.stateManager.Switch("attack");
    }

    public void OnExit(Player owner)
    {

    }
}
