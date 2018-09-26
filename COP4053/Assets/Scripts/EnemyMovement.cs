using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : Movement
{
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Animate();
    }

    void Animate()
    {
        animator.Play("Idle");
    }
}
