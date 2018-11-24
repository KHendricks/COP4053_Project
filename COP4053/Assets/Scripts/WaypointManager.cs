using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour 
{
    public GameObject wanderingArea;
    public GuardEnemy enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WanderEnemy")
        {
            MoveWaypoint();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "WanderEnemy")
        {
            MoveWaypoint();
        }
    }

    void MoveWaypoint()
    {
        if (enemy.spotted == false)
        {
            gameObject.transform.position = RandomPointInBounds(wanderingArea.GetComponent<BoxCollider>().bounds);
        }
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
