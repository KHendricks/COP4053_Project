using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    float timer = 16f;
    
    private YieldInstruction fadeInstruction = new YieldInstruction();
    public float fadeTime;
    public Image image;

    // Make this static and just the name of the scene
    public static string currLevel;

    // Use this for initialization
    void Start()
    {
        //Get name of scene and set currLevel
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "WinScreen" && sceneName != "LoseScreen" && sceneName != "MainMenu")
        {
            currLevel = sceneName;
        }
        if(sceneName == "FinalCutscene")
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartCoroutine(FadeIn());
                SceneManager.LoadScene("WinScreen");
            }
        }

    }


    public void Reload()
    {
        StartCoroutine(FadeIn());
        SceneManager.LoadScene(currLevel);
        //StartCoroutine(FadeOut());
        FindObjectOfType<AudioManager>().Stop("Lose");
        FindObjectOfType<AudioManager>().Unmute("Footsteps");
    }


    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }

}
