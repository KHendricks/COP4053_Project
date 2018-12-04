using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    float timer = 16f;

    // Make this static and just the name of the scene
    public static string currLevel;

    // Use this for initialization
    void Start()
    {
        //Get name of scene and set currLevel

    }

    // Update is called once per frame
    void Update()
    {
        var name = SceneManager.GetActiveScene().name;
        if (name != "WinScreen" && name != "LoseScreen" && name != "MainMenu")
        {
            currLevel = name;
        }
        if(name == "FinalCutscene")
        {
            FindObjectOfType<AudioManager>().Play("Theme");
            timer -= Time.deltaTime;
            if (timer <= 0)
                SceneManager.LoadScene("WinScreen");
        }
            
    }


    public void Reload()
    {
        SceneManager.LoadScene(currLevel);
        FindObjectOfType<AudioManager>().Stop("Lose");
        FindObjectOfType<AudioManager>().Unmute("Footsteps");
    }

}
