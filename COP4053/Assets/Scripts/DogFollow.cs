using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogFollow : MonoBehaviour 
{
    public float distanceFromPlayer;

    public GameObject player;
    public float speed = 3f; 

	// Update is called once per frame
	void Update () 
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Move our position a step closer to the target, if player is far enough away
        if (distance > distanceFromPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
    }
}
