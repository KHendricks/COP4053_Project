﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    bool playerExit, dogExit;
    public GameObject spawnPoint;
    private GameObject playerObject, dogObject;
    Player player;
    public int level;
    public int dogCount;
    int followCount;

    private void Start()
    {
        playerExit = dogExit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                int DoesPlayerHaveSlingshot = PlayerPrefs.GetInt("Slingshot");
                int DoesPlayerHaveKnife = PlayerPrefs.GetInt("Knife");
                foreach(Dog dog in player.dogs)
                {
                    if (dog.followPlayer)
                        followCount++;
                }
                switch (level)
                {
                    case 1:
                        if (DoesPlayerHaveSlingshot == 1)
                            playerExit = true;
                        break;
                    case 2:
                        if (DoesPlayerHaveKnife == 1)
                            playerExit = true;
                        break;
                    default:
                        playerExit = true;
                        break;
                }
            }
            else
                Debug.Log("Player is null.");


        }
        else if (other.gameObject.tag == "Dog")
        {
            dogExit = true;
        }


        if (dogExit && playerExit && followCount == dogCount)
        {
            player.transform.position = spawnPoint.transform.position;
            foreach (Dog dog in player.dogs)
            {
                dog.transform.position = spawnPoint.transform.position;
            }
            player.lastSpawnPoint = spawnPoint;
        }

        //if (dogExit && playerExit && DoesPlayerHaveSlingshot == 1)
        //{
        //    playerObject.transform.position = spawnPoint.transform.position;
        //    dogObject.transform.position = spawnPoint.transform.position;

        //    player = playerObject.GetComponent<Player>();
        //    if (player != null)
        //        player.lastSpawnPoint = spawnPoint;
        //}
    }
}
