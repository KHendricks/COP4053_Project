using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour 
{
    public GameObject[] inventorySlots;

	// Use this for initialization
	void Start ()
    {
        // If on first level loads the values for the inventory to empty
        if (SceneManager.GetActiveScene().name == "Desert_level1")
        {
            PlayerPrefs.SetInt("Lasso", 0);
            PlayerPrefs.SetInt("Knife", 0);
            PlayerPrefs.SetInt("Whip", 0);
            PlayerPrefs.SetInt("Boomerang", 0);
            PlayerPrefs.SetInt("Slingshot", 0);
        }

        LoadInventory();
	}

    void LoadInventory()
    {
        // Loads the inventory UI based on the playerprefs
        if (PlayerPrefs.GetInt("Lasso") == 1)
        {
            inventorySlots[0].SetActive(true);
        }
        else
        {
            inventorySlots[0].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Knife") == 1)
        {
            inventorySlots[1].SetActive(true);
        }
        else
        {
            inventorySlots[1].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Whip") == 1)
        {
            inventorySlots[2].SetActive(true);
        }
        else
        {
            inventorySlots[2].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Boomerang") == 1)
        {
            inventorySlots[3].SetActive(true);
        }
        else
        {
            inventorySlots[3].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Slingshot") == 1)
        {
            inventorySlots[4].SetActive(true);
        }
        else
        {
            inventorySlots[4].SetActive(false);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
