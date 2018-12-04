using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState : IState<Enemy> {
    
    public void OnEnter(Enemy owner) 
    {

    }

    public void Update(Enemy owner)
    {
        owner.animator.Play("Idle");
        if (owner.spotted)
            owner.stateManager.Switch("attack");
    }

    public void OnExit(Enemy owner)
    {

    }
}
