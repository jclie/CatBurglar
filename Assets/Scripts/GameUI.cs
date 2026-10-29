using UnityEngine;
using TMPro;

// Updates the gameplay UI with information
// from the GameManager and PlayerInventory
public class GameUI : MonoBehaviour
{
    // Text displaying the player's current score
    public TMP_Text scoreText;

    // Text displaying the remaining game time
    public TMP_Text timerText;

    // Text displaying the item currently being carried
    public TMP_Text carryingText;

    // Text displaying how many times the player was caught
    public TMP_Text caughtText;

    // Reference to the GameManager
    private GameManager gameManager;

    // Reference to the Player's inventory
    private PlayerInventory playerInventory;

    // Start runs once when the game begins
    void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindAnyObjectByType<GameManager>();

        // Find the PlayerInventory in the scene
        playerInventory = FindAnyObjectByType<PlayerInventory>();
    }

    // Update runs once every frame
    void Update()
    {
        // Make sure the GameManager exists
        if (gameManager != null)
        {
            // Display the current score
            scoreText.text =
                "Score: " + gameManager.score;

            // Convert the remaining seconds into
            // minutes and seconds
            int minutes =
                Mathf.FloorToInt(gameManager.timeRemaining / 60f);

            int seconds =
                Mathf.FloorToInt(gameManager.timeRemaining % 60f);

            // Display time as something like 1:32
            timerText.text =
                "Time: " +
                minutes.ToString("00") +
                ":" +
                seconds.ToString("00");

            // Display the number of times caught
            caughtText.text =
                "Caught: " + gameManager.timesCaught;
        }

        // Make sure the PlayerInventory exists
        if (playerInventory != null)
        {
            // Check whether the Player is carrying loot
            if (playerInventory.IsCarryingItem())
            {
                // Display the carried item's name
                carryingText.text =
                    "Carrying: " +
                    playerInventory.carriedItemName;
            }
            else
            {
                // Player currently has no loot
                carryingText.text = "Carrying: Nothing";
            }
        }
    }
}