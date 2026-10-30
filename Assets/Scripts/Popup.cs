using UnityEngine;
using TMPro;

// Controls the interaction popup above the Player
public class Popup : MonoBehaviour
{
    // Text displayed inside the popup
    public TMP_Text promptText;

    // Reference to the Player
    public Transform player;

    // Distance above the Player
    public Vector3 offset = new Vector3(0f, 1.2f, 0f);

    // Main Camera
    private Camera mainCamera;

    // Controls whether the popup is visible
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Find the Main Camera
        mainCamera = Camera.main;

        // Get the CanvasGroup attached to this object
        canvasGroup = GetComponent<CanvasGroup>();

        // Hide the popup when the game starts
        HidePrompt();
    }

    void LateUpdate()
    {
        // Make sure required references exist
        if (player == null || mainCamera == null)
        {
            return;
        }

        // Position the popup above the Player
        Vector3 worldPosition =
            player.position + offset;

        transform.position =
            mainCamera.WorldToScreenPoint(worldPosition);
    }

    // Shows the popup
    public void ShowPrompt(string message)
    {
        // Change the popup message
        promptText.text = message;

        // Make the popup visible
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }

    // Hides the popup
    public void HidePrompt()
    {
        // Make the popup invisible
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }
}