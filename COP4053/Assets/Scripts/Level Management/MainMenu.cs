using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public int currLevel;

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
            FindObjectOfType<SceneLoader>().Reload();
        }
    }

    public void OnContinue()
    {
        switch (currLevel)
        {
            case 1:
                LoadLevelOne();
                break;
            case 2:
                LoadLevelTwo();
                break;
            case 3:
                LoadLevelThree();
                break;
            case 4:
                LoadLevelFour();
                break;
            default:
                Debug.Log("Please specify level number.");
                break;
        }
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Desert_level1");

        // Was muted in PlayerStats.cs Death()
        FindObjectOfType<AudioManager>().Unmute("Footsteps");

    }

    void LoadLevelTwo()
    {
        SceneManager.LoadScene("Canyon_level2");

        // Was muted in PlayerStats.cs Death()
        FindObjectOfType<AudioManager>().Unmute("Footsteps");
    }

    void LoadLevelThree()
    {
        SceneManager.LoadScene("Switchbacks_level3");

        // Was muted in PlayerStats.cs Death()
        FindObjectOfType<AudioManager>().Unmute("Footsteps");
    }

    void LoadLevelFour()
    {
        SceneManager.LoadScene("Cabin_level4");

        // Was muted in PlayerStats.cs Death()
        FindObjectOfType<AudioManager>().Unmute("Footsteps");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<AudioManager>().Stop("Lose");
        FindObjectOfType<AudioManager>().Play("Theme");
    }
}
