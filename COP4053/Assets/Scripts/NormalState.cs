using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : IState<Ranger> {

    public void OnEnter(Ranger owner)
    {

    }

    // Update is called once per frame
    public void Update (Ranger owner) 
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

    public void OnExit(Ranger owner)
    {

    }
}
