using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
    void Update () {
        transform.LookAt(Camera.main.transform.position);

        Quaternion rot = transform.rotation;
        float offset = rot.eulerAngles.y + 180;
        rot.eulerAngles = new Vector3(rot.eulerAngles.x, offset, rot.eulerAngles.z);
        transform.rotation = rot;
    }
}
