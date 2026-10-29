using UnityEngine;
using UnityEngine.InputSystem;

// Controls an object that the Player can use as a hiding spot
public class HidingSpot : MonoBehaviour
{
    // Tracks whether the Player is close enough
    // to interact with this hiding spot
    private bool playerNearby = false;

    // Stores a reference to the Player
    private GameObject player;

    // Stores the PlayerMovement component
    private PlayerMovement playerMovement;

    // Stores the Player's SpriteRenderer
    // so we can visually show when the Player is hiding
    private SpriteRenderer playerSprite;

    // Update runs once every frame
    void Update()
    {
        // Do nothing if the Player is not nearby
        if (!playerNearby || player == null)
        {
            return;
        }

        // Check whether the Player pressed E
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            // If already hiding, leave the hiding spot
            if (playerMovement.isHiding)
            {
                ExitHiding();
            }
            else
            {
                // Otherwise enter the hiding spot
                EnterHiding();
            }
        }
    }

    // Runs when the Player enters the interaction area
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to the Player
        if (other.CompareTag("Player"))
        {
            // Remember that the Player is nearby
            playerNearby = true;

            // Store the Player GameObject
            player = other.gameObject;

            // Get the Player's movement component
            playerMovement =
                player.GetComponent<PlayerMovement>();

            // Get the Player's SpriteRenderer
            playerSprite =
                player.GetComponent<SpriteRenderer>();

            Debug.Log("Press E to hide.");
        }
    }

    // Runs when the Player leaves the interaction area
    private void OnTriggerExit2D(Collider2D other)
    {
        // Only react to the Player
        if (other.CompareTag("Player"))
        {
            // Don't clear the references while the Player
            // is currently hiding
            if (playerMovement != null &&
                playerMovement.isHiding)
            {
                return;
            }

            playerNearby = false;
            player = null;
            playerMovement = null;
            playerSprite = null;
        }
    }

    // Places the Player into the hiding state
    private void EnterHiding()
    {
        // Make sure the PlayerMovement component exists
        if (playerMovement == null)
        {
            return;
        }

        // Mark the Player as hidden
        playerMovement.isHiding = true;

        // Make the Player partly transparent
        // This gives us temporary visual feedback
        if (playerSprite != null)
        {
            Color color = playerSprite.color;
            color.a = 0.35f;
            playerSprite.color = color;
        }

        // Find the AudioManager and play the pickup sound
        AudioManager audioManager =
            FindAnyObjectByType<AudioManager>();

        if (audioManager != null)
        {
            audioManager.PlayHide();
        }

        Debug.Log("Player is hiding.");
    }

    // Removes the Player from the hiding state
    private void ExitHiding()
    {
        // Make sure the PlayerMovement component exists
        if (playerMovement == null)
        {
            return;
        }

        // Player is no longer hidden
        playerMovement.isHiding = false;

        // Restore the Player's normal appearance
        if (playerSprite != null)
        {
            Color color = playerSprite.color;
            color.a = 1f;
            playerSprite.color = color;
        }

        Debug.Log("Player stopped hiding.");
    }
}