using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    bool playerExit, dogExit;
    public GameObject spawnPoint;
    private GameObject playerObject, dogObject;
    Player player;

    private void Start()
    {
        playerExit = dogExit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObject = other.gameObject;
            playerExit = true;
        }
        else if (other.gameObject.tag == "Dog")
        {
            dogObject = other.gameObject;
            dogExit = true;
        }

        int DoesPlayerHaveSlingshot = PlayerPrefs.GetInt("Slingshot");
        if (dogExit && playerExit && DoesPlayerHaveSlingshot == 1)
        {
            playerObject.transform.position = spawnPoint.transform.position;
            dogObject.transform.position = spawnPoint.transform.position;

            player = playerObject.GetComponent<Player>();
            if (player != null)
                player.lastSpawnPoint = spawnPoint;
        }
    }
}
