using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<Enemy> {

    public void OnEnter(Enemy owner)
    {

    }

    public void Update(Enemy owner)
    {
        owner.Chase();

        if (owner.isStationary)
        {
            owner.animator.Play("Idle");
        }
        else if(!owner.isStationary)
        {
            owner.animator.Play("Walk");
        }

        owner.TryAttack();
    }

    public void OnExit(Enemy owner)
    {

    }
}
