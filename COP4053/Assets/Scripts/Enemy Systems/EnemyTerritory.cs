using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritory : MonoBehaviour {

    public Enemy enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enemy.spotted = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemy.spotted = false;
            if (enemy.wanderEnabled)
            {
                enemy.stateManager.Switch("patrol");
            }
            else
                enemy.stateManager.Switch("guard");
        }

    }
}
