using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAggroTerritory : MonoBehaviour 
{
    // This script is to allow the EnemyTerritory to follow the movement
    // of the player
	public GameObject enemyToFollow;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if(enemyToFollow != null)
		    gameObject.transform.position = new Vector3(enemyToFollow.transform.position.x, enemyToFollow.transform.position.y, enemyToFollow.transform.position.z);
	}
}
