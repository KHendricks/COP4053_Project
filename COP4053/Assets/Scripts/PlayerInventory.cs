using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public GameObject player;
    public GameObject[] inventorySlots;
    public GameObject selector;

    // Weapon prefabs to load
    public GameObject slingshotPrefab;

    // Variables where the prefabs are loaded
    public GameObject slingshot;

    public bool slingshotDisplayed;

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

        // Defaults the display of the weapons to false
        slingshotDisplayed = false;

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
                // Destroy the nonselected weapons
                DestroySlingshot();

                // Need to check if slot is active first (this is to ensure the
                // the player has the item)
                if (!inventorySlots[0].activeSelf)
                {
                    break;
                }
                break;

            // Knife
            case 1:
                // Destroy the nonselected weapons
                DestroySlingshot();

                if (!inventorySlots[1].activeSelf)
                {
                    break;
                }
                break;

            // Whip
            case 2:
                // Destroy the nonselected weapons
                DestroySlingshot();

                if (!inventorySlots[2].activeSelf)
                {
                    break;
                }
                break;

            // Boomerang
            case 3:
                // Destroy the nonselected weapons
                DestroySlingshot();

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

                // Spawn the slingshot at the player's position
                if (!slingshotDisplayed)
                {
                    slingshotDisplayed = true;
                    slingshot = Instantiate(slingshotPrefab, player.transform.position, Quaternion.identity);
                }

                // Keep the slingshot attached to the player
                float slingshotOffet = .5f;
                if (slingshotDisplayed)
                {
                    slingshot.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + slingshotOffet, player.transform.position.z);
                }

                break;
        }
    }

    void DestroySlingshot()
    {
        Destroy(slingshot);

        // Ensures only one slingshot will be active
        slingshotDisplayed = false;
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
        // JoystickButton4 is the left bumper on an Xbox one controller
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
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
        // JoystickButton5 is the right bumper on an Xbox one controller
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
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
