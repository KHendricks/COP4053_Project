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
        tipList.Add("Beware of blackhats! The villainous dognappers will do anything to protect their treasure.");
        tipList.Add("Have you rescued the dog and found your slingshot?");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialog.Deactivate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
}
