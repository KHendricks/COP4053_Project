using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCenter : MonoBehaviour {

    public Player player;
    public ContextMessage context;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Dog")
        {
            var dog = other.gameObject.GetComponent<Dog>();

                if (dog != null && dog.rescued)
                {
                    dog.closeEnough = true;
                    if (dog.followPlayer)
                        context.Activate("stay", InputAction.FollowToggle);
                    else
                        context.Activate("come", InputAction.FollowToggle);
                }
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Dog")
        {
            var dog = other.gameObject.GetComponent<Dog>();
            if(dog != null)
            {
                dog.closeEnough = false;
                context.Deactivate();
            }
        }
    }
}
