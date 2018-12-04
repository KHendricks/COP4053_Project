using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingObstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BossRock" || other.gameObject.tag == "SlingshotRock")
        {
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
