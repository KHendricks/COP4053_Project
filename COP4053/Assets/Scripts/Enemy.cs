using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour 
{
    public int health;

    // Assign dropIndex to corresponding item 
    // to get drop from enemy
    public int dropIndex;
    public GameObject[] dropItem;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SlingshotRock")
        {
            Destroy(other.gameObject);
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

    void DropItem()
    {
        if (dropIndex != -1)
        {
            Instantiate(dropItem[dropIndex], gameObject.transform.position, Quaternion.identity);
        }
    }
}
