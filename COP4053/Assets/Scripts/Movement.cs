using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    public Vector2 currentDirection;
    public bool isStationary;
    public float speed = 3f;
    protected Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        currentDirection = new Vector2();
        isStationary = true;
	}

    // Determine what direction the character is facing and whether they
    // are moving.
    protected void SetFacing(Vector3 dir)
    {
        if (!Mathf.Approximately(dir.z, 0f) && !Mathf.Approximately(dir.x, 0f))
        {
            currentDirection.Set(-dir.x, dir.z);
            isStationary = false;
        }
        else
            isStationary = true;
    }
}
