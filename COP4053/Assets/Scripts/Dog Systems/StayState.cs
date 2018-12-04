using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayState : IState<Dog> {

    public void OnEnter(Dog owner)
    {
        GameObject.FindObjectOfType<AudioManager>().Play("Woof");
    }

    // Update is called once per frame
    public void Update(Dog owner)
    {
        if (owner.followPlayer)
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
