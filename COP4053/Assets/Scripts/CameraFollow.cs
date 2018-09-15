using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    public Transform target;
    public float smooth = 5.0f;
    public float xOffset, yOffset = 2f, zOffset;

	// Update is called once per frame
	void Update () 
    {
        Vector3 newTargetPosition = new Vector3(target.position.x + xOffset, target.position.y + yOffset, target.position.z + zOffset);
        transform.position = Vector3.Lerp(transform.position, newTargetPosition, Time.deltaTime * smooth);
    }
}
