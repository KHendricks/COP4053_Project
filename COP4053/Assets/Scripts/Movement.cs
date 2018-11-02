using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    public Vector2 currentDirection;
    public bool isStationary;
    public float speed = 3f;
    protected Rigidbody rb;
    protected Vector3 prevPos;

    // Use this for initialization
    protected void Awake () {
        rb = gameObject.GetComponent<Rigidbody>();
        currentDirection = new Vector2();
        isStationary = true;
        prevPos = transform.position;
	}

    // Determine what direction the character is facing and whether they
    // are moving.
    protected void SetFacing(Vector3 dir)
    {
        if (!Mathf.Approximately(dir.z, 0f) && !Mathf.Approximately(dir.x, 0f))
        {
            currentDirection.Set(-dir.x, dir.z);
        }

        isStationary = AtRest();
    }

    protected bool AtRest()
    {
        var temp = transform.position == prevPos;
        prevPos = transform.position;
        return temp;
    }

    public void NormalizeDirection(Vector3 playerPos, Vector3 pos)
    {

        Vector3 dip = (playerPos - pos).normalized;
        SetFacing(dip);
    }
}
