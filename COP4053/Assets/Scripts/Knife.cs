using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Movement
{
    // Have the rock prefab loaded into this variable
    // through the editor
    public GameObject knifeRockPrefab;
    public float rockSpeed = 3f;

    private bool enableFire, mutex;
    private Vector3 dir;

    // Use this for initialization
    void Start()
    {
        enableFire = mutex = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Hides the knife when not being fired
        if (mutex)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        UpdateDirection();
        FireKnife();
    }

    public void FireKnife()
    {
        // On enter press instantiate a rock projectile
        if (InputManager.JustPressed(InputAction.Attack) && enableFire)
        {
            if (mutex)
            {
                mutex = false;
                StartCoroutine("EnableAttack");
            }

            // When player swaps inventory slots, direction gets reset to zero and if player
            // doesn't move the shot will be stationary. This check is to prevent that shot
            // from occuring
            if (dir.normalized != Vector3.zero)
            {
                // Shows the knife when attacking
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                GameObject knifeProjectile = Instantiate(knifeRockPrefab, gameObject.transform.position, Quaternion.identity);
                knifeProjectile.GetComponent<Rigidbody>().velocity = dir.normalized * rockSpeed;
                Destroy(knifeProjectile, 1f);
            }
        }
    }

    public void UpdateDirection()
    {
        // This gives a warning but because the vector will only be 0 when the player
        // is stationary this is the specific case needing to be check. This effectively
        // saves the previous direction
        if (!Mathf.Approximately(PlayerPrefs.GetFloat("PlayerDirectionX"), 0f) && !Mathf.Approximately(PlayerPrefs.GetFloat("PlayerDirectionZ"), 0f))
        {
            dir = new Vector3(PlayerPrefs.GetFloat("PlayerDirectionX"), PlayerPrefs.GetFloat("PlayerDirectionY"), PlayerPrefs.GetFloat("PlayerDirectionZ"));
        }
    }

    IEnumerator EnableAttack()
    {
        enableFire = false;
        yield return new WaitForSeconds(.7f);
        enableFire = mutex = true;
    }
}
