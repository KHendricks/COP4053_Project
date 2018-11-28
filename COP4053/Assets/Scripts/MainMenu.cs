using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
        // Play Theme music
        FindObjectOfType<AudioManager>().Play("Theme");
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyUp(KeyCode.JoystickButton0) && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            LoadLevelOne();
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton0) && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("WinScreen"))
        {
            GameOver();
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton0) && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LoseScreen"))
        {
            GameOver();
        }
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Desert_level1");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<AudioManager>().Stop("Lose");
        FindObjectOfType<AudioManager>().Play("Theme");
    }
}
