using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotFire : Movement
{
    // Have the rock prefab loaded into this variable
    // through the editor
    public GameObject slingshotRockPrefab;
    public float rockSpeed = 3f;

    private bool enableFire, mutex;
    private Vector3 dir;

	// Use this for initialization
	void Start ()
    {
        enableFire = mutex = true;
	}

	// Update is called once per frame
	void Update ()
    {
        FireSlingshot();
        UpdateDirection();
	}

    public void FireSlingshot()
    {
        int ammo = PlayerPrefs.GetInt("SlingshotAmmo");
        // On enter press instantiate a rock projectile
        if (Input.GetKeyDown(KeyCode.Return) && enableFire && ammo > 0)
        {
            if (mutex)
            {
                mutex = false;
                StartCoroutine("EnableShot");
            }

            // Sets the ammo
            PlayerPrefs.SetInt("SlingshotAmmo", ammo - 1);

            GameObject rockProjectile = Instantiate(slingshotRockPrefab, gameObject.transform.position, Quaternion.identity);
            rockProjectile.GetComponent<Rigidbody>().velocity = dir.normalized * rockSpeed;
            Destroy(rockProjectile, 5);
        }
    }

    public void UpdateDirection()
    {
        // This gives a warning but because the vector will only be 0 when the player
        // is stationary this is the specific case needing to be check. This effectively
        // saves the previous direction
        if (PlayerPrefs.GetFloat("PlayerDirectionX") != 0f && PlayerPrefs.GetFloat("PlayerDirectionZ") != 0f)
        {
            dir = new Vector3(PlayerPrefs.GetFloat("PlayerDirectionX"), PlayerPrefs.GetFloat("PlayerDirectionY"), PlayerPrefs.GetFloat("PlayerDirectionZ"));
        }
    }

    IEnumerator EnableShot()
    {
        enableFire = false;
        yield return new WaitForSeconds(.7f);
        enableFire = mutex = true;
    }
}
