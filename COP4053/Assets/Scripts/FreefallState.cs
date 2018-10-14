using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreefallState : IState<Ranger>
{

    public void OnEnter(Ranger owner)
    {
        owner.animator.Play("Jump");
    }

    // Update is called once per frame
    public void Update(Ranger owner)
    {
        owner.Movement();
        if(owner.IsGrounded())
            owner.stateManager.Switch("normal");
    }

    public void OnExit(Ranger owner)
    {

    }
}
