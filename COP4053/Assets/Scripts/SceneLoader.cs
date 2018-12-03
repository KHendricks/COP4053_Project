using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

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

    }

    public void LoadLevel(int level)
    {

    }

    public void Reload()
    {
        SceneManager.LoadScene(currLevel);
    }

}
