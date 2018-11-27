using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasso : MonoBehaviour {

    // Have the rock prefab loaded into this variable
    // through the editor
    public GameObject lassoPrefab, player;
    public float rockSpeed = 3f;

    private bool enableFire, mutex;
    private Vector3 dir;
    private bool oneProjectile;

    // Use this for initialization
    void Start()
    {
        enableFire = mutex = true;
        player = GameObject.Find("PlayerContainer");
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
        UseLasso();
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

    void UseLasso()
    {
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
                oneProjectile = false;

                // Shows the knife when attacking
                gameObject.transform.GetChild(0).gameObject.SetActive(true);

                // "Moves" the lasso forward
                if (!oneProjectile)
                {
                    GameObject lassoProjectile = Instantiate(lassoPrefab, gameObject.transform.position, Quaternion.identity);
                    lassoProjectile.GetComponent<Rigidbody>().velocity = dir.normalized * rockSpeed;
                    StartCoroutine(MovePlayer(lassoProjectile));
                }
            }
        }
    }

    IEnumerator EnableAttack()
    {
        enableFire = false;
        yield return new WaitForSeconds(.7f);
        enableFire = mutex = true;
    }

    IEnumerator MovePlayer(GameObject lassoProjectile)
    {

        yield return new WaitForSeconds(.8f);

        try
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, lassoProjectile.transform.position, 100f * Time.deltaTime);
            Destroy(lassoProjectile);
        }
        catch (System.Exception e)
        {
            Debug.Log("Lasso projectile destroyed from hitting collider. You will" +
                      " not teleport to destination location");
        }

        oneProjectile = true;
    }
}
