using UnityEngine;

// Keeps track of the loot item the player is currently carrying
public class PlayerInventory : MonoBehaviour
{
    // Stores the name of the item currently being carried
    // An empty string means the player is not carrying anything
    public string carriedItemName = "";

    // Stores the point value of the currently carried item
    public int carriedItemValue = 0;

    // Returns true if the player is carrying an item
    public bool IsCarryingItem()
    {
        return carriedItemName != "";
    }

    // Attempts to pick up a loot item
    public bool PickUpItem(string itemName, int pointValue)
    {
        // Do not allow another item if the player
        // is already carrying something
        if (IsCarryingItem())
        {
            Debug.Log(
                "You are already carrying " +
                carriedItemName +
                "!"
            );

            return false;
        }

        // Store the item's information
        carriedItemName = itemName;
        carriedItemValue = pointValue;

        // Temporary message for testing
        Debug.Log(
            "Now carrying " +
            carriedItemName +
            " worth " +
            carriedItemValue +
            " points."
        );

        return true;
    }

    // Removes the item currently being carried
    public void ClearCarriedItem()
    {
        // Clear the item name
        carriedItemName = "";

        // Reset its value
        carriedItemValue = 0;

        // Temporary testing message
        Debug.Log("Inventory is now empty.");
    }

    // Removes the player's carried loot when they get caught
    public void LoseCarriedItem()
    {
        // Check whether the player was actually carrying something
        if (IsCarryingItem())
        {
            // Tell us which item was lost
            Debug.Log(
                "Lost " +
                carriedItemName +
                " because the player was caught!"
            );
        }

        // Remove the carried item's information
        carriedItemName = "";

        // Reset its point value
        carriedItemValue = 0;
    }
}