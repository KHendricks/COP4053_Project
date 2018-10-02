using System.Collections;
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
	void Update () 
    {
        ChangeSelection();
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
