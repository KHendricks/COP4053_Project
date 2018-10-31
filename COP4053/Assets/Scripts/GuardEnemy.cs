﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class GuardEnemy : Movement {

    public int dropIndex;
    public GameObject[] dropItem;
    public Animator animator;
    public StateManager<GuardEnemy> stateManager;
    public int health;
    public GameObject knifeForInventory;
    public bool spotted;
    public Player player;
    public float distanceFromPlayer;

    // Use this for initialization
    void Start () {
        spotted = false;
        distanceFromPlayer = 0.5f;
        animator = GetComponentInChildren<Animator>();
        stateManager = new StateManager<GuardEnemy>();
        stateManager.Add("guard", new GuardState());
        stateManager.Add("attack", new GuardAttackState());

        stateManager.Switch("guard");
    }
	
	// Update is called once per frame
	void Update () {
        stateManager.Update(this);
	}

    void OnTriggerEnter(Collider other)
    {
        // When attacked by slingshot projectile
        if (other.gameObject.tag == "SlingshotRock")
        {
            Destroy(other.gameObject);
            health -= PlayerPrefs.GetInt("PlayerBaseDamage");
            KillEnemy();
        }

        if (other.gameObject.tag == "Knife")
        {
            health -= PlayerPrefs.GetInt("PlayerBaseDamage");
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            DropItem();
        }
    }
    
    public void Chase()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Move our position a step closer to the target, if player is far enough away
        if (distance > distanceFromPlayer)
        {
            // Lerp instead of MoveTowards to smooth out the transition.
            //var dir = Vector3.MoveTowards(transform.position, player.transform.position, step);
            var dir = Vector3.Lerp(transform.position, player.transform.position, step);
            transform.position = dir;
        }

        Vector3 newDir = new Vector3(PlayerPrefs.GetFloat("PlayerDirectionX"), PlayerPrefs.GetFloat("PlayerDirectionY"), PlayerPrefs.GetFloat("PlayerDirectionZ"));
        Vector3 dip = (player.transform.position - transform.position).normalized;
        SetFacing(dip);

        animator.SetFloat("FaceZ", currentDirection.y);
        animator.SetFloat("FaceX", currentDirection.x);

        if (isStationary)
        {
            animator.Play("Idle");
        }
        else if(!isStationary)
        {
            animator.Play("Walk");
        }
    }

    public void TryAttack()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= distanceFromPlayer)
        {
            Attack();
        }
    }

    public void Attack()
    {
        // use whatever weapon he has to attack player
    }

    void DropItem()
    {
        if (dropIndex != -1)
        {
            Instantiate(dropItem[dropIndex], gameObject.transform.position, Quaternion.identity);
        }
    }
}