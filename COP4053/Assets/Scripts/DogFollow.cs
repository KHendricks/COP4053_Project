using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollow : Movement 
{
    public float distanceFromPlayer;
    public GameObject player;
    Animator animator;

	// Update is called once per frame
	void Update () 
    {
        Follow();
        Animate();
    }

    void Follow ()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Move our position a step closer to the target, if player is far enough away
        if (distance > distanceFromPlayer)
        {
            // Lerp instead of MoveTowards to smooth out the transition.
            //var dir = Vector3.MoveTowards(transform.position, player.transform.position, step);
            var dir = Vector3.Lerp(transform.position, player.transform.position, step);
            transform.position = dir;
            SetFacing(dir);
        }
    }

    void Animate()
    {
        animator.SetFloat("FaceZ", currentDirection.y);
        animator.SetFloat("FaceX", currentDirection.x);

        if (!isStationary)
            animator.Play("Trot");
    }
}
