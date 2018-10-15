using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreefallState : IState<Player>
{

    public void OnEnter(Player owner)
    {
        owner.animator.Play("Jump");
    }

    // Update is called once per frame
    public void Update(Player owner)
    {
        owner.Movement();
        if(owner.IsGrounded())
            owner.stateManager.Switch("normal");
    }

    public void OnExit(Player owner)
    {

    }
}
