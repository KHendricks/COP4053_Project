using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    bool playerExit, dogExit;

    private void Start()
    {
        playerExit = dogExit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerExit = true;
        }
        else if (other.gameObject.tag == "Dog")
        {
            dogExit = true;
        }

        if (dogExit && playerExit)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
