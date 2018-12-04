﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : Movement 
{
    public int dropIndex;
    public GameObject[] dropItem;
    public GameObject knifeForInventory;
    public int health;

    public Animator animator;
    public StateManager<Enemy> stateManager;

    public Player player;
    public bool spotted;
    private float distanceFromPlayer;
    public bool isAttacking;
    private bool notAttackedRecently;
    private bool WaitForAttackTimer, FlashTimer;
    private Material guardEnemyMaterial;
    public SpriteRenderer playerSpriteRenderer;

    public bool wanderEnabled;
    public GameObject waypoint1;
    public GameObject waypoint2;
    GameObject currWaypoint;


    // Use this for initialization
    void Start()
    {
        currWaypoint = waypoint1;
        guardEnemyMaterial = GetComponent<SpriteRenderer>().material;

        notAttackedRecently = WaitForAttackTimer = FlashTimer = true;

        spotted = false;
        distanceFromPlayer = 0.5f;
        animator = GetComponentInChildren<Animator>();
        stateManager = new StateManager<Enemy>();
        stateManager.Add("guard", new GuardState());
        stateManager.Add("attack", new EnemyAttackState());
        stateManager.Add("patrol", new PatrolState());

        if (wanderEnabled)
        {
            stateManager.Switch("patrol");
        }
        else
            stateManager.Switch("guard");
    }

    // Update is called once per frame
    void Update()
    {
        stateManager.Update(this);
    }

    void OnTriggerEnter(Collider other)
    {
        // When attacked by slingshot projectile
        if (other.gameObject.tag == "SlingshotRock")
        {
            stateManager.Switch("attack");
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("EnemyHurt");
            health -= PlayerPrefs.GetInt("PlayerBaseDamage");
            KillEnemy();

            // Makes enemy flash red
            if (FlashTimer)
            {
                FlashTimer = false;
                StartCoroutine(Flash(guardEnemyMaterial));
            }
        }

        if (other.gameObject.tag == "Knife")
        {
            stateManager.Switch("attack");
            FindObjectOfType<AudioManager>().Play("EnemyHurt");
            health -= PlayerPrefs.GetInt("PlayerBaseDamage");
            KillEnemy();

            // Makes enemy flash red
            if (FlashTimer)
            {
                FlashTimer = false;
                StartCoroutine(Flash(guardEnemyMaterial));
            }
        }
    }

    void KillEnemy()
    {
        if (health <= 0)
        {
            playerSpriteRenderer.material.SetColor("_Color", Color.white);
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

        NormalizeDirection(player.transform.position, transform.position);
        animator.SetFloat("FaceZ", currentDirection.y);
        animator.SetFloat("FaceX", currentDirection.x);
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

        // For now I'm just setting it to deduct one health from the player
        // and locks it from happening again for 2 seconds
        if (notAttackedRecently)
        {
            isAttacking = true;
            //animator.Play("Attack");
            Debug.Log(PlayerPrefs.GetInt("PlayerHealth"));
            PlayerPrefs.SetInt("PlayerHealth", PlayerPrefs.GetInt("PlayerHealth") - 1);
            Debug.Log(PlayerPrefs.GetInt("PlayerHealth"));
            notAttackedRecently = false;
            FindObjectOfType<AudioManager>().Play("PlayerHurt");
        }
        isAttacking = false;
        if (WaitForAttackTimer)
        {
            WaitForAttackTimer = false;
            StartCoroutine("AttackTimer");
        }
    }

    void DropItem()
    {
        if (dropIndex != -1)
        {
            Instantiate(dropItem[dropIndex], gameObject.transform.position, Quaternion.identity);
        }
    }

    IEnumerator AttackTimer()
    {
        StartCoroutine(Flash(playerSpriteRenderer.material));
        yield return new WaitForSeconds(2f);
        notAttackedRecently = WaitForAttackTimer = true;
    }

    // The variable being passed is the spriteRenderer material of the object
    // that needs to "Flash"
    IEnumerator Flash(Material material)
    {
        material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.33f);
        material.SetColor("_Color", Color.white);
        yield return new WaitForSeconds(0.33f);

        FlashTimer = true;
    }

    //public void Wander()
    //{
    //    Debug.Log("toggle = " + toggle);
    //    var waypoint = (toggle % 2 == 0) ? waypoint1 : waypoint2;

    //    var dir = Vector3.MoveTowards(transform.position, waypoint.transform.position, 1f * Time.deltaTime);
    //    transform.position = dir;

    //    NormalizeDirection(waypoint.transform.position, transform.position);
    //    animator.SetFloat("FaceZ", currentDirection.y);
    //    animator.SetFloat("FaceX", currentDirection.x);
    //}

    public void Patrol()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, currWaypoint.transform.position);

        // When waypoint is reached, switch to other waypoint.
        if (distance < 0.2f)
        {
            currWaypoint = currWaypoint == waypoint1 ? waypoint2 : waypoint1;
        }

        var dir = Vector3.MoveTowards(transform.position, currWaypoint.transform.position, step);
        transform.position = dir;

        NormalizeDirection(currWaypoint.transform.position, transform.position);
        animator.SetFloat("FaceZ", currentDirection.y);
        animator.SetFloat("FaceX", currentDirection.x);
    }
}