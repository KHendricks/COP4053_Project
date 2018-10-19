using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This script is to track the player's damage, health, score, and other
// features along those lines

public class PlayerStats : MonoBehaviour 
{
    public Text playerHealth, playerScore, playerAmmo;
    public int startingHealth, startingDamage;
    public GameObject[] healthDisplay;

	// Use this for initialization
	void Start () 
    {
        // Initialize values to default at the start of the game
        if (SceneManager.GetActiveScene().name == "Desert_level1")
        {
            PlayerPrefs.SetInt("PlayerHealth", startingHealth);
            PlayerPrefs.SetInt("PlayerScore", 0);
            PlayerPrefs.SetInt("PlayerBaseDamage", startingDamage);

            // Slingshot ammo
            PlayerPrefs.SetInt("SlingshotAmmo", 30);
        }

        // Update the UI
        playerHealth.text = PlayerPrefs.GetInt("PlayerHealth").ToString();
        playerScore.text = PlayerPrefs.GetInt("PlayerScore").ToString();
        playerAmmo.text = PlayerPrefs.GetInt("SlingshotAmmo").ToString();

	}
	
	// Update is called once per frame
	void Update () 
    {
        PlayerPrefs.SetInt("PlayerHealth", startingHealth);
        playerAmmo.text = PlayerPrefs.GetInt("SlingshotAmmo").ToString();
        UpdateHealth();
	}

    void UpdateHealth()
    {
        if (PlayerPrefs.GetInt("PlayerHealth") == 3)
        {
            healthDisplay[0].SetActive(true);
            healthDisplay[1].SetActive(false);
            healthDisplay[2].SetActive(false);
            healthDisplay[3].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("PlayerHealth") == 2)
        {
            healthDisplay[0].SetActive(false);
            healthDisplay[1].SetActive(true);
            healthDisplay[2].SetActive(false);
            healthDisplay[3].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("PlayerHealth") == 1)
        {
            healthDisplay[0].SetActive(false);
            healthDisplay[1].SetActive(false);
            healthDisplay[2].SetActive(true);
            healthDisplay[3].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("PlayerHealth") == 0)
        {
            healthDisplay[0].SetActive(false);
            healthDisplay[1].SetActive(false);
            healthDisplay[2].SetActive(false);
            healthDisplay[3].SetActive(true);
        }
    }
}
