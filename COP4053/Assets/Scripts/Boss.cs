using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour 
{

    public int health;
    public BossArea bossArea;
    public GameObject player;

    // Attackable weapons from boss
    public GameObject bossRock;

    private bool FlashTimer, ableToAttack;
    private Material bossMaterial;
    private float attackDelay = 3f;
    private bool rockTimer, oneRockTimer, charging, determineCharge, chargeTimer;

	// Use this for initialization
	void Start () 
    {
        chargeTimer = determineCharge = oneRockTimer = ableToAttack = FlashTimer = true;
        bossMaterial = GetComponent<SpriteRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // If the player is inside the boss' area enable the encounter
        if (bossArea.GetStatus())
        {
            int attackToUse = 2;
            if (ableToAttack)
            {
                ableToAttack = false;

                attackToUse = Random.Range(1, 4);
                // Types of attacks the boss can do
                if (attackToUse == 1)
                {
                    SingleShot();
                }
                else if (attackToUse == 2)
                {
                    MultiShot();
                }
                else if (attackToUse == 3)
                {
                    Barrage();
                }
                // Rarely will 4 show up in the range, if it does just do a 
                // single shot
                else
                {
                    SingleShot();
                }

                // This determines the timer on how frequently the boss can attack
                StartCoroutine(ResetAttackDelay());
            }

            if (determineCharge)
            {
                determineCharge = false;
                StartCoroutine(DetermineCharge());
            }

            if (charging)
            {
                if (chargeTimer)
                {
                    chargeTimer = false;
                    StartCoroutine(EndCharge());
                }
                Charge();
            }
        }
	}

    void SingleShot()
    {
        Vector3 startingRockPos = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
        float rockSpeed = 4f;

        // Spawns the rock from the boss
        GameObject rockProjectile = Instantiate(bossRock, startingRockPos, Quaternion.identity);
        Vector3 playerPosWhenShot = player.transform.position;

        // Constantly shoot objects towards player
        Vector3 pathVector = Vector3.Normalize(player.transform.position - startingRockPos);
        rockProjectile.GetComponent<Rigidbody>().velocity = pathVector * rockSpeed;


        Destroy(rockProjectile, attackDelay + 2f);
    }

    void MultiShot()
    {
        float rockSpeed = 4f;
        float xOffset = 1f;
        int numRocks = 3;

        for (int i = 0; i < numRocks; i++)
        {
            // Spawns the rock from the boss
            Vector3 startingRockPos = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
            GameObject rockProjectile = Instantiate(bossRock, startingRockPos, Quaternion.identity);
            Vector3 playerPosWhenShot = player.transform.position;

            // Constantly shoot objects towards player
            Vector3 tempPlayPos = new Vector3(player.transform.position.x + (xOffset * i), player.transform.position.y, player.transform.position.z + (xOffset * i));
            Vector3 pathVector = Vector3.Normalize(tempPlayPos - startingRockPos);
            rockProjectile.GetComponent<Rigidbody>().velocity = pathVector * rockSpeed;

            Destroy(rockProjectile, attackDelay + 2f);
        }
    }

    // Calls the barrage helper to spawn a shot with a delay
    void Barrage()
    {
        InvokeRepeating("BarrageHelper", 0f, .4f);
        StartCoroutine(CancelBarrage());
    }

    void BarrageHelper()
    {
        Vector3 startingRockPos = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
        float rockSpeed = 4f;

        // Spawns the rock from the boss
        GameObject rockProjectile = Instantiate(bossRock, startingRockPos, Quaternion.identity);
        Vector3 playerPosWhenShot = player.transform.position;

        // Constantly shoot objects towards player
        Vector3 pathVector = Vector3.Normalize(player.transform.position - startingRockPos);
        rockProjectile.GetComponent<Rigidbody>().velocity = pathVector * rockSpeed;

        Destroy(rockProjectile, attackDelay + 2f);
    }

    void Charge()
    {
        float chargeSpeed = 2f;

        Vector3 pathVector = player.transform.position - gameObject.transform.position;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position, chargeSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // When attacked by slingshot projectile
        if (other.gameObject.tag == "SlingshotRock")
        {
            Destroy(other.gameObject);
            health -= PlayerPrefs.GetInt("PlayerBaseDamage");
            KillEnemy();

            // Makes enemy flash red
            if (FlashTimer)
            {
                FlashTimer = false;
                StartCoroutine(Flash(bossMaterial));
            }
        }

        if (other.gameObject.tag == "Knife")
        {
            health -= PlayerPrefs.GetInt("PlayerBaseDamage");
            KillEnemy();

            // Makes enemy flash red
            if (FlashTimer)
            {
                FlashTimer = false;
                StartCoroutine(Flash(bossMaterial));
            }
        }
    }

    void KillEnemy()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
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

    IEnumerator ResetAttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        ableToAttack = true;
    }

    IEnumerator CancelBarrage()
    {
        yield return new WaitForSeconds(1f);
        CancelInvoke();
    }

    IEnumerator DetermineCharge()
    {
        yield return new WaitForSeconds(4f);

        // Code to randomize if charging
        determineCharge = true;

        int chanceToCharge = Random.Range(0, 2);

        if (chanceToCharge == 0)
        {
            charging = true;
        }
        else
        {
            Debug.Log("Failed to charge");
            charging = false;
        }
    }

    IEnumerator EndCharge()
    {
        yield return new WaitForSeconds(.5f);

        // Code to randomize if charging
        chargeTimer = true;
        charging = false;
    }
}
