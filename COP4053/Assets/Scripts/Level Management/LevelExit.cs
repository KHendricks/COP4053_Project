using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelExit : MonoBehaviour {

    bool playerExit, dogExit;
    public GameObject spawnPoint;
    Player player;
    public int level;
    public int dogCount;
    int followCount;
    private YieldInstruction fadeInstruction = new YieldInstruction();
    public float fadeTime;
    public Image image;

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
                    case 4:
                        if (player.defeatedBoss)
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

        if(level == 3)
        {
            if (playerExit && player.dogs.Count == dogCount)
                LoadLevelFour();
        }
        else if(level == 4)
        {
            if (playerExit && player.dogs.Count == dogCount)
                GameOver();
        }
        else if (dogExit && playerExit && followCount == dogCount)
        {
            switch(level)
            {
                case 1:
                    LoadLevelTwo();
                    break;
                case 2:
                    LoadLevelThree();
                    break;
                default:
                    Debug.Log("Please specify level 1 or 2.");
                    break;
            }
        }
    }

    void LoadLevelTwo()
    {
        StartCoroutine(FadeIn());
        SceneManager.LoadScene("Canyon_level2");

        // Was muted in PlayerStats.cs Death()
        FindObjectOfType<AudioManager>().Unmute("Footsteps");
    }

    void LoadLevelThree()
    {
        StartCoroutine(FadeIn());
        SceneManager.LoadScene("Switchbacks_level3");

        // Was muted in PlayerStats.cs Death()
        FindObjectOfType<AudioManager>().Unmute("Footsteps");
    }

    void LoadLevelFour()
    {
        StartCoroutine(FadeIn());
        SceneManager.LoadScene("Cabin_level4");

        // Was muted in PlayerStats.cs Death()
        FindObjectOfType<AudioManager>().Unmute("Footsteps");
    }

    void GameOver()
    {
        StartCoroutine(FadeIn());
        SceneManager.LoadScene("FinalCutscene");
        FindObjectOfType<AudioManager>().Mute("Footsteps");
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
