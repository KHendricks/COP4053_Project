using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Player player;
    public GameObject image;
	// Use this for initialization
	void Start () {
        image.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (player.hasKey)
        {
            image.SetActive(true);
        }
        else
            image.SetActive(false);
	}
}
