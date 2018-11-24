using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour 
{
    public DialogMessage dialog;
    public ContextMessage context;
    public int tipIndex;

    private List<string> tipList;
	// Use this for initialization
	void Start () 
    {
        tipList = new List<string>();
        tipList.Add("Rescue the dog and find your slingshot.");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        dialog.Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!dialog.active)
        {
            if (tipIndex >= 0)
            {
                dialog.Activate(tipList[tipIndex]);
            }
        }
    }
}
