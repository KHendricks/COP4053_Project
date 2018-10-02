﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour 
{
    public GameObject[] inventorySlots;
    public GameObject selector;

    // These are hard coded x positions for the frame selector

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

            PlayerPrefs.SetInt("InventorySlotSelected", 0);
        }

        LoadInventory();
	}

    // Update is called once per frame
    void Update()
    {
        // This function allows for Q + E to rotate the selector
        ChangeSelection();

        // This function updates the inventory UI if the player picks up a 
        // corresponding inventory item
        LoadInventory();

        // This function is to allow the player to display the current
        // item selected
        // TODO: This is a WIP
        DisplayCurrent();
    }

    void DisplayCurrent()
    {
        // This number will hold the index of the inventory
        // 0: Lasso
        // 1: Knife
        // 2: Whip
        // 3: Boomerang
        // 4: Slingshot
        int selectorIndex = PlayerPrefs.GetInt("InventorySlotSelected");

        switch(selectorIndex)
        {
            // Lasso
            case 0:
                // Need to check if slot is active first (this is to ensure the
                // the player has the item)
                if (!inventorySlots[0].activeSelf)
                {
                    break;
                }
                break;

            // Knife
            case 1:
                if (!inventorySlots[1].activeSelf)
                {
                    break;
                }
                break;

            // Whip
            case 2:
                if (!inventorySlots[2].activeSelf)
                {
                    break;
                }
                break;

            // Boomerang
            case 3:
                if (!inventorySlots[3].activeSelf)
                {
                    break;
                }
                break;

            // Slingshot
            case 4:
                if (!inventorySlots[4].activeSelf)
                {
                    break;
                }
                break;
        }
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

    void ChangeSelection()
    {
        // Cycle selector to the left
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int selectorIndex = (PlayerPrefs.GetInt("InventorySlotSelected") - 1);
            if (selectorIndex <= -1)
            {
                selectorIndex = 4;
            }
         
            selector.transform.position = new Vector3(inventorySlots[selectorIndex].transform.position.x, selector.transform.position.y, selector.transform.position.z);
            PlayerPrefs.SetInt("InventorySlotSelected", selectorIndex);
        }

        // Cycle selector to the right
        if (Input.GetKeyDown(KeyCode.E))
        {
            int selectorIndex = (PlayerPrefs.GetInt("InventorySlotSelected") + 1);
            if (selectorIndex >= 5)
            {
                selectorIndex = 0;
            }

            selector.transform.position = new Vector3(inventorySlots[selectorIndex].transform.position.x, selector.transform.position.y, selector.transform.position.z);
            PlayerPrefs.SetInt("InventorySlotSelected", selectorIndex);
        }
    }
}