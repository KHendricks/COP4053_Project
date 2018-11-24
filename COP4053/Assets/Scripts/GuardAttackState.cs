using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttackState : IState<GuardEnemy> {

    public void OnEnter(GuardEnemy owner)
    {

    }

    public void Update(GuardEnemy owner)
    {
        owner.Chase();

        if (owner.isStationary && !owner.isAttacking)
        {
            owner.animator.Play("Idle");
        }
        else if(!owner.isStationary && !owner.isAttacking)
        {
            owner.animator.Play("Walk");
        }

        owner.TryAttack();
    }

    public void OnExit(GuardEnemy owner)
    {

    }
}
