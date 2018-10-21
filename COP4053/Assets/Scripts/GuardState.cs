using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState : IState<GuardEnemy> {

    public void OnEnter(GuardEnemy owner) 
    {

    }

    public void Update(GuardEnemy owner)
    {
        owner.animator.Play("Idle");
    }



    public void OnExit(GuardEnemy owner)
    {

    }
}
