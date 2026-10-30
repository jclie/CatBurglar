using UnityEngine;

// Keeps track of the loot item the player is currently carrying
public class PlayerInventory : MonoBehaviour
{
    // Stores the name of the item currently being carried
    // An empty string means the player is not carrying anything
    public string carriedItemName = "";

    // Stores the point value of the currently carried item
    public int carriedItemValue = 0;

    // Stores the actual loot GameObject currently being carried
    public GameObject carriedLootObject;

    // Returns true if the player is carrying an item
    public bool IsCarryingItem()
    {
        return carriedItemName != "";
    }

    // Attempts to pick up a loot item
    public bool PickUpItem(
        string itemName,
        int pointValue,
        GameObject lootObject)
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

        // Store the item's name
        carriedItemName = itemName;

        // Store the item's point value
        carriedItemValue = pointValue;

        // Store the actual loot GameObject
        // so it can be returned if the player gets caught
        carriedLootObject = lootObject;

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

    // Removes the item after it has been
    // successfully deposited at CatHome
    public void ClearCarriedItem()
    {
        // Clear the item name
        carriedItemName = "";

        // Reset its value
        carriedItemValue = 0;

        // Forget the loot GameObject
        // Do NOT reactivate it because it was successfully stolen
        carriedLootObject = null;

        // Temporary testing message
        Debug.Log("Inventory is now empty.");
    }

    // Removes the player's carried loot when they get caught
    public void LoseCarriedItem()
    {
        // Check whether there is an actual loot object being carried
        if (carriedLootObject != null)
        {
            // Make the loot appear again at its original location
            carriedLootObject.SetActive(true);

            // Tell us which item was returned
            Debug.Log(
                carriedItemName +
                " returned to its original location."
            );
        }

        // Remove the carried item's name
        carriedItemName = "";

        // Reset its point value
        carriedItemValue = 0;

        // Clear the stored loot GameObject
        carriedLootObject = null;
    }
}