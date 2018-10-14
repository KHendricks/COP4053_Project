using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Ranger>
{

    public void OnEnter(Ranger owner)
    {
        owner.animator.Play("Attack");
    }

    // Update is called once per frame
    public void Update(Ranger owner)
    {
        // Switch back to normal state when animation is over.
        if (owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            owner.stateManager.Switch("normal");
    }

    public void OnExit(Ranger owner)
    {

    }
}
