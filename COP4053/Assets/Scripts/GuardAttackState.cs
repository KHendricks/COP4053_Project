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
        owner.TryAttack();
    }

    public void OnExit(GuardEnemy owner)
    {

    }
}
