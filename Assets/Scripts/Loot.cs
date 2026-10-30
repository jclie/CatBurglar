using UnityEngine;
using UnityEngine.InputSystem;

// Controls a loot object that the player can steal
public class Loot : MonoBehaviour
{
    // Name of this loot item
    public string itemName = "TvRemote";

    // Number of points this loot item is worth
    public int pointValue = 50;

    // Tracks whether the player is close enough to interact
    private bool playerNearby = false;

    // Stores a reference to the Player's inventory
    private PlayerInventory playerInventory;

    // Reference to the interaction popup
    private Popup popup;

    // Start runs once when the loot object is created
    void Start()
    {
        // Find the interaction popup in the scene
        popup = FindAnyObjectByType<Popup>();

        // Warn us if the popup could not be found
        if (popup == null)
        {
            Debug.LogWarning("Popup could not be found.");
        }
    }

    // Update runs once every frame
    void Update()
    {
        // Check that the player is nearby before allowing interaction
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
        }
    }

    // Runs when another 2D collider enters the loot's trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check whether the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // Player is close enough to interact
            playerNearby = true;

            // Get the PlayerInventory component from the Player
            playerInventory = other.GetComponent<PlayerInventory>();

            // Show the interaction popup
            if (popup != null)
            {
                popup.ShowPrompt("[E] Steal " + itemName);
            }

            // Tell us that interaction is available
            Debug.Log("Press E to steal " + itemName);
        }
    }

    // Runs when another 2D collider leaves the loot's trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check whether the Player left the trigger
        if (other.CompareTag("Player"))
        {
            // Player can no longer interact with this loot
            playerNearby = false;

            // Clear the inventory reference
            playerInventory = null;

            // Hide the interaction popup
            if (popup != null)
            {
                popup.HidePrompt();
            }
        }
    }

    // Attempts to pick up this loot item
    private void PickUp()
    {
        // Make sure we successfully found the PlayerInventory component
        if (playerInventory == null)
        {
            Debug.LogWarning("PlayerInventory could not be found.");
            return;
        }

        // Ask the PlayerInventory to pick up this item
        // Try to give this loot item to the Player
        bool pickupSuccessful =
            playerInventory.PickUpItem(
                itemName,
                pointValue,
                gameObject
            );

        // Only remove the loot from the floor if pickup succeeded
        if (pickupSuccessful)
        {
            // Hide the interaction popup because
            // the loot has been picked up
            if (popup != null)
            {
                popup.HidePrompt();
            }
            // Find the AudioManager and play the pickup sound
            AudioManager audioManager =
                FindAnyObjectByType<AudioManager>();

            if (audioManager != null)
            {
                audioManager.PlayLootPickup();
            }

            Debug.Log("Picked up " + itemName + "!");

            // Hide this loot object
            gameObject.SetActive(false);
        }
    }
}