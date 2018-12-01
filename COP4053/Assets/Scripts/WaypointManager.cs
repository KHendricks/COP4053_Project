using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour 
{
    public GameObject wanderingArea1;
    public GameObject wanderingArea2;
    public Enemy enemy;
    int toggle;

	// Use this for initialization
	void Start () {
        toggle = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WanderEnemy")
        {
            //enemy.toggle += 1;
            //MoveWaypoint();
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "WanderEnemy")
    //    {
    //        MoveWaypoint();
    //    }
    //}

    void MoveWaypoint()
    {
        // Patrolling back and forth between 2 points
        toggle += 1;
        if(toggle%2 == 0)
            gameObject.transform.position = RandomPointInBounds(wanderingArea1.GetComponent<BoxCollider>().bounds);
        else
            gameObject.transform.position = RandomPointInBounds(wanderingArea2.GetComponent<BoxCollider>().bounds);
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.center.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
