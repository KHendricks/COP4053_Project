using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject player;
    public GameObject[] inventorySlots;
    public GameObject selector;
    public ContextMessage context;

    // Weapon prefabs to load
    public GameObject slingshotPrefab, knifePrefab, lassoPrefab;

    // Variables where the prefabs are loaded
    private GameObject slingshot, knife, lasso;

    private bool slingshotDisplayed, knifeDisplayed, lassoDisplayed;
    public GameObject slingshotAmmoAmount;

	// Use this for initialization
	void Start ()
    {
        // If on first level loads the values for the inventory to empty
        if (SceneManager.GetActiveScene().name == "Desert_level1")
        {
            PlayerPrefs.SetInt("Lasso", 0);
            PlayerPrefs.SetInt("Knife", 0);
            PlayerPrefs.SetInt("Slingshot", 0);

            PlayerPrefs.SetInt("InventorySlotSelected", 0);
        }

        // Defaults the display of the weapons to false
        slingshotDisplayed = knifeDisplayed = lassoDisplayed = false;

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

        if (Input.GetKeyDown(KeyCode.P))
        {
            GiveKnife();
            GiveSlingshot();
        }
    }

    void DisplayCurrent()
    {
        // This number will hold the index of the inventory
        // 0: Lasso
        // 1: Knife
        // 2: Slingshot
        int selectorIndex = PlayerPrefs.GetInt("InventorySlotSelected");

        switch(selectorIndex)
        {
            // Lasso
            case 2:
                // Destroy the nonselected weapons
                DestroySlingshot();
                DestroyKnife();

                // Need to check if slot is active first (this is to ensure the
                // the player has the item)
                if (!inventorySlots[2].activeSelf)
                {
                    break;
                }

                // Spawn the lasso at the player's position
                if (!lassoDisplayed)
                {
                    lassoDisplayed = true;
                    lasso = Instantiate(lassoPrefab, player.transform.position, Quaternion.identity);
                }

                // Keep the lasso attached to the player
                float lassoOffset = .2f;
                if (lassoDisplayed)
                {
                    float xDir = PlayerPrefs.GetFloat("PlayerDirectionX") / 6;
                    float zDir = PlayerPrefs.GetFloat("PlayerDirectionZ") / 6;
                    lasso.transform.position = new Vector3(player.transform.position.x + xDir, player.transform.position.y + lassoOffset, player.transform.position.z + zDir);
                }

                break;

            // Knife
            case 1:
                // Destroy the nonselected weapons
                DestroySlingshot();
                DestroyLasso();

                if (!inventorySlots[1].activeSelf)
                {
                    break;
                }

                // Spawn the slingshot at the player's position
                if (!knifeDisplayed)
                {
                    knifeDisplayed = true;
                    knife = Instantiate(knifePrefab, player.transform.position, Quaternion.identity);
                    context.Activate("attack", InputAction.Attack);
                }

                // Keep the slingshot attached to the player
                float knifeOffset = .2f;
                if (knifeDisplayed)
                {
                    float xDir = PlayerPrefs.GetFloat("PlayerDirectionX") / 6;
                    float zDir = PlayerPrefs.GetFloat("PlayerDirectionZ") / 6;
                    knife.transform.position = new Vector3(player.transform.position.x + xDir, player.transform.position.y + knifeOffset, player.transform.position.z + zDir);
                }

                break;

            // Slingshot
            case 0:
                // Destroy the nonselected weapons
                DestroyKnife();
                DestroyLasso();

                if (!inventorySlots[0].activeSelf)
                {
                    break;
                }

                // Spawn the slingshot at the player's position
                if (!slingshotDisplayed)
                {
                    slingshotDisplayed = true;
                    slingshot = Instantiate(slingshotPrefab, player.transform.position, Quaternion.identity);
                    context.Activate("shoot", InputAction.Attack);
                }

                // Keep the slingshot attached to the player
                float slingshotOffet = .1f;
                if (slingshotDisplayed)
                {
                    float xDir = PlayerPrefs.GetFloat("PlayerDirectionX") / 6;
                    float zDir = PlayerPrefs.GetFloat("PlayerDirectionZ") / 6;
                    slingshot.transform.position = new Vector3(player.transform.position.x + xDir, player.transform.position.y + slingshotOffet, player.transform.position.z + zDir);
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

    void DestroyKnife()
    {
        Destroy(knife);

        // Ensures only one knife will be active
        knifeDisplayed = false;
    }

    void DestroyLasso()
    {
        Destroy(lasso);
        lassoDisplayed = false;
    }

    void LoadInventory()
    {
        // Loads the inventory UI based on the playerprefs
        if (PlayerPrefs.GetInt("Lasso") == 1)
        {
            inventorySlots[2].SetActive(true);
        }
        else
        {
            inventorySlots[2].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Knife") == 1)
        {
            inventorySlots[1].SetActive(true);
        }
        else
        {
            inventorySlots[1].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Slingshot") == 1)
        {
            inventorySlots[0].SetActive(true);
            slingshotAmmoAmount.SetActive(true);
        }
        else
        {
            inventorySlots[0].SetActive(false);
            slingshotAmmoAmount.SetActive(false);
        }
    }

    void ChangeSelection()
    {
        // Cycle selector to the left
        // JoystickButton4 is the left bumper on an Xbox one controller
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            int prevIndex = PlayerPrefs.GetInt("InventorySlotSelected");
            int selectorIndex = (PlayerPrefs.GetInt("InventorySlotSelected") - 1);

            // This if statement stops the selector from going to positions without 
            // an active weapon

            if (selectorIndex <= -1)
            {
                selectorIndex = 2;

                if (inventorySlots[selectorIndex].activeSelf)
                {
                    selector.transform.position = new Vector3(inventorySlots[selectorIndex].transform.position.x, selector.transform.position.y, selector.transform.position.z);
                    PlayerPrefs.SetInt("InventorySlotSelected", selectorIndex);
                }
                else
                {
                    PlayerPrefs.SetInt("InventorySlotSelected", prevIndex);
                }
            }
            else
            {
                if (inventorySlots[selectorIndex].activeSelf)
                {
                    selector.transform.position = new Vector3(inventorySlots[selectorIndex].transform.position.x, selector.transform.position.y, selector.transform.position.z);
                    PlayerPrefs.SetInt("InventorySlotSelected", selectorIndex);
                }
            }


        }

        // Cycle selector to the right
        // JoystickButton5 is the right bumper on an Xbox one controller
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            int prevIndex = PlayerPrefs.GetInt("InventorySlotSelected");
            int selectorIndex = (PlayerPrefs.GetInt("InventorySlotSelected") + 1);

            // This if statement stops the selector from going to positions without 
            // an active weapon
            if (selectorIndex >= 3)
            {
                selectorIndex = 0;

                if (inventorySlots[selectorIndex].activeSelf)
                {
                    selector.transform.position = new Vector3(inventorySlots[selectorIndex].transform.position.x, selector.transform.position.y, selector.transform.position.z);
                    PlayerPrefs.SetInt("InventorySlotSelected", selectorIndex);
                }
                else
                {
                    PlayerPrefs.SetInt("InventorySlotSelected", prevIndex);
                }
            }
            else
            {
                if (inventorySlots[selectorIndex].activeSelf)
                {
                    selector.transform.position = new Vector3(inventorySlots[selectorIndex].transform.position.x, selector.transform.position.y, selector.transform.position.z);
                    PlayerPrefs.SetInt("InventorySlotSelected", selectorIndex);
                }
            }
        }
    }

    void GiveKnife()
    {
        PlayerPrefs.SetInt("Knife", 1);
    }

    void GiveSlingshot()
    {
        PlayerPrefs.SetInt("Slingshot", 1);
    }
}
