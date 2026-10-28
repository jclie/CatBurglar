using UnityEngine;
using UnityEngine.InputSystem;

// Controls the area where the player returns stolen loot
public class DepositZone : MonoBehaviour
{
    // Tracks whether the player is inside the deposit area
    private bool playerNearby = false;

    // Stores a reference to the player's inventory
    private PlayerInventory playerInventory;

    // Stores a reference to the GameManager
    private GameManager gameManager;

    // Start runs once when the game begins
    void Start()
    {
        // Find the GameManager in the current scene
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update runs once every frame
    void Update()
    {
        // Deposit loot when the player is nearby
        // and presses the E key
        if (playerNearby &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            DepositItem();
        }
    }

    // Runs when something enters the CatHome trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to the Player
        if (other.CompareTag("Player"))
        {
            // Player is close enough to deposit
            playerNearby = true;

            // Get the PlayerInventory attached to the Player
            playerInventory =
                other.GetComponent<PlayerInventory>();

            // Check whether the player actually has loot
            if (playerInventory != null &&
                playerInventory.IsCarryingItem())
            {
                Debug.Log(
                    "Press E to deposit " +
                    playerInventory.carriedItemName
                );
            }
        }
    }

    // Runs when something leaves the CatHome trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Only react to the Player
        if (other.CompareTag("Player"))
        {
            // Player is no longer close enough
            playerNearby = false;

            // Remove our reference to the inventory
            playerInventory = null;
        }
    }

    // Deposits the item the player is carrying
    private void DepositItem()
    {
        // Make sure we found the PlayerInventory
        if (playerInventory == null)
        {
            return;
        }

        // Make sure the player is actually carrying something
        if (!playerInventory.IsCarryingItem())
        {
            Debug.Log("You are not carrying any loot.");
            return;
        }

        // Save the item's information before clearing it
        string depositedItem =
            playerInventory.carriedItemName;

        int depositedValue =
            playerInventory.carriedItemValue;

        // Add the loot value to the player's score
        gameManager.AddScore(depositedValue);

        // Empty the player's inventory
        playerInventory.ClearCarriedItem();

        // Confirm that the deposit worked
        Debug.Log(
            "Deposited " +
            depositedItem +
            " for " +
            depositedValue +
            " points!"
        );
    }
}