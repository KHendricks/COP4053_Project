using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool Interact()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }

    public bool Dismiss()
    {
        return Input.GetKeyDown(KeyCode.X);
    }

}
