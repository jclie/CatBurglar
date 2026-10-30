using UnityEngine;
using UnityEngine.SceneManagement;


// Manages important information for the current game,
// including score, times caught, and the game timer
public class GameManager : MonoBehaviour
{
    // Stores the final score so the Results scene
    // can access it after MainGame closes
    public static int finalScore = 0;

    // Stores the final number of times caught
    // for the Results scene
    public static int finalTimesCaught = 0;

    // Stores the player's total score
    public int score = 0;

    // Stores how many times the player has been caught
    public int timesCaught = 0;

    // Total amount of time the player has to complete the game
    // 120 seconds = 2 minutes
    public float gameTime = 120f;

    // Stores how much time is currently remaining
    public float timeRemaining;

    // Tracks whether the game is currently active
    public bool gameActive = true;

    // Stores the last second that played a timer warning sound
    // so the sound only plays once per second
    private int lastWarningSecond = -1;

    // Start runs once when the game begins
    void Start()
    {
        // Start the countdown at the full game time
        timeRemaining = gameTime;
    }

    // Update runs once every frame
    void Update()
    {
        // Only count down while the game is active
        if (gameActive)
        {
            // Subtract the amount of time that passed
            // since the previous frame
            timeRemaining -= Time.deltaTime;

            // Check whether the game is in its final 10 seconds
            if (timeRemaining <= 10f && timeRemaining > 0f)
            {
                // Convert the remaining time into a whole number
                int currentSecond = Mathf.CeilToInt(timeRemaining);

                // Only play one warning sound for each second
                if (currentSecond != lastWarningSecond)
                {
                    // Find the AudioManager
                    AudioManager audioManager =
                        FindAnyObjectByType<AudioManager>();

                    // Play the timer warning sound
                    if (audioManager != null)
                    {
                        audioManager.PlayTimerRunningLow();
                    }

                    // Remember which second already played a sound
                    lastWarningSecond = currentSecond;
                }
            }

            // Prevent the timer from going below zero
            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;

                // End the game when the timer reaches zero
                EndGame();
            }
        }
    }

    // Adds points when loot is successfully deposited
    public void AddScore(int points)
    {
        // Increase the player's score
        score += points;

        // Temporary message for testing
        Debug.Log("Current Score: " + score);
    }

    // Records when the player gets caught
    public void PlayerCaught()
    {
        // Increase the caught counter
        timesCaught++;

        // Temporary message for testing
        Debug.Log("Times Caught: " + timesCaught);
    }

    // Ends the current game
    private void EndGame()
    {
        // Stop gameplay from continuing
        gameActive = false;

        // Save the final results before changing scenes
        finalScore = score;
        finalTimesCaught = timesCaught;

        // Find the AudioManager and play the pickup sound
        AudioManager audioManager =
            FindAnyObjectByType<AudioManager>();

        if (audioManager != null)
        {
            audioManager.PlayGameOver();
        }

        // Confirm the results in the Console
        Debug.Log("GAME OVER!");
        Debug.Log("Final Score: " + finalScore);
        Debug.Log("Times Caught: " + finalTimesCaught);

        // Load the Results screen
        SceneManager.LoadScene("ResultScene");
    }
}