using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    public Transform MyCameraTransform;
    private Transform MyTransform;
    public bool alignNotLook = true;

	// Use this for initialization
	void Start () {
        MyTransform = this.transform;
        MyCameraTransform = Camera.main.transform;
    }
	
	// Update is called once per frame
    void Update () {
        if (alignNotLook)
            MyTransform.forward = MyCameraTransform.forward;
        else
            MyTransform.LookAt(MyCameraTransform, Vector3.up);
        //transform.LookAt(Camera.main.transform.position);

        //Quaternion rot = transform.rotation;
        //float offset = rot.eulerAngles.y + 180;
        //rot.eulerAngles = new Vector3(rot.eulerAngles.x, offset, rot.eulerAngles.z);
        //transform.rotation = rot;
    }
}
