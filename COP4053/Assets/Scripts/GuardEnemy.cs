﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class GuardEnemy : Movement {

    public Animator animator;
    public StateManager<GuardEnemy> stateManager;
    public int health;
    public GameObject knifeForInventory;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        stateManager = new StateManager<GuardEnemy>();
        stateManager.Add("guard", new GuardState());

        stateManager.Switch("guard");
    }
	
	// Update is called once per frame
	void Update () {
        stateManager.Update(this);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SlingshotRock")
        {
            Destroy(other.gameObject);
            health -= PlayerPrefs.GetInt("SlingshotDamage");
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        if (health <= 0)
        {
            Destroy(gameObject);

            // If on first level let enemy drop knife
            if (SceneManager.GetActiveScene().name == "Desert_level1")
            {
                DropKnife();
            }
        }
    }

    void DropKnife()
    {
        Instantiate(knifeForInventory, gameObject.transform.position, Quaternion.identity);
    }
}
