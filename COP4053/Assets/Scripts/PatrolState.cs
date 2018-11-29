using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy> {

    public void OnEnter(Enemy owner)
    {

    }

    public void Update(Enemy owner)
    {
        owner.Wander();
        owner.animator.Play("Walk");
        if (owner.spotted)
            owner.stateManager.Switch("attack");
    }

    public void OnExit(Enemy owner)
    {

    }
}
