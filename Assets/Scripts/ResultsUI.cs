using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Controls the Results screen
// and displays the player's final game results
public class ResultsUI : MonoBehaviour
{
    // Displays the player's final score
    public TMP_Text finalScoreText;

    // Displays how many times the player was caught
    public TMP_Text timesCaughtText;

    // Start runs once when the Results scene opens
    void Start()
    {
        // Display the final score saved by GameManager
        finalScoreText.text =
            "Total Stolen Value: " +
            GameManager.finalScore;

        // Display the final caught count
        timesCaughtText.text =
            "Times Caught: " +
            GameManager.finalTimesCaught;
    }

    // Starts a new game
    public void PlayAgain()
    {
        // Load the main gameplay scene
        SceneManager.LoadScene("MainGame");
    }
}