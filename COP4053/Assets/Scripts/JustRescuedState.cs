using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustRescuedState : IState<Dog>
{

    public void OnEnter(Dog owner)
    {

    }

    // Update is called once per frame
    public void Update(Dog owner)
    {
        owner.Follow();
        if (owner.player.isStationary)
            owner.animator.Play("Idle");
        else
            owner.animator.Play("Trot");

        if (owner.player.dogsFollow)
        {
            owner.stateManager.Switch("follow");
        }
        else
        {
            owner.animator.Play("Idle");
        }
    }

    public void OnExit(Dog owner)
    {

    }

}
