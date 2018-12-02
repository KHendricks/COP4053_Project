using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagedState : IState<Dog>
{

    public void OnEnter(Dog owner)
    {

    }

    // Update is called once per frame
    public void Update(Dog owner)
    {
        if (owner.rescued)
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