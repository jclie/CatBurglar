using UnityEngine;

// Controls the homeowner's vision, detection timer,
// wall blocking, and catching the player
public class HomeownerVision : MonoBehaviour
{
    // True when the player is currently inside
    // the homeowner's vision trigger
    private bool playerInsideVision = false;

    // True when the homeowner currently has
    // a clear view of the player
    public bool playerDetected = false;

    // Stores a reference to the player's Transform
    private Transform player;

    // Walls on this layer block the homeowner's vision
    public LayerMask wallLayer;

    // How long the homeowner must see the player
    // before the player is caught
    public float timeToCatch = 1.5f;

    // Tracks how long the player has been visible
    public float detectionTimer = 0f;

    // Position where the player returns after being caught
    public Transform playerStart;

    // Update runs once every frame
    void Update()
    {
        // If the player is not inside the vision trigger,
        // there is nothing to detect
        if (!playerInsideVision || player == null)
        {
            playerDetected = false;
            detectionTimer = 0f;
            return;
        }

        // Check whether a wall is blocking the player
        CheckLineOfSight();

        // If the homeowner has a clear view of the player,
        // increase the detection timer
        if (playerDetected)
        {
            detectionTimer += Time.deltaTime;

            // Catch the player once they have been
            // visible for the required amount of time
            if (detectionTimer >= timeToCatch)
            {
                CatchPlayer();
            }
        }
        else
        {
            // Reset detection progress when sight is lost
            detectionTimer = 0f;
        }
    }

    // Runs when another 2D collider enters
    // the homeowner's vision trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to the Player
        if (other.CompareTag("Player"))
        {
            // Remember that the Player is inside the vision area
            playerInsideVision = true;

            // Store a reference to the Player
            player = other.transform;

            Debug.Log("Player entered vision.");
        }
    }

    // Runs when another 2D collider leaves
    // the homeowner's vision trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Only react to the Player
        if (other.CompareTag("Player"))
        {
            // Player is no longer inside the vision area
            playerInsideVision = false;

            // Stop detecting the Player
            playerDetected = false;

            // Reset the detection timer
            detectionTimer = 0f;

            // Clear the Player reference
            player = null;

            Debug.Log("Player left vision.");
        }
    }

    // Checks whether a wall is between
    // the homeowner and the Player
    private void CheckLineOfSight()
    {
        // Direction from the Vision object to the Player
        Vector2 direction =
            player.position - transform.position;

        // Distance between the Vision object and the Player
        float distance = direction.magnitude;

        // Check for a wall between the homeowner and Player
        RaycastHit2D wallHit = Physics2D.Raycast(
            transform.position,
            direction.normalized,
            distance,
            wallLayer
        );

        // If no wall was hit, the homeowner can see the Player
        if (wallHit.collider == null)
        {
            playerDetected = true;
        }
        else
        {
            // A wall is blocking the Player
            playerDetected = false;
        }
    }

    // Handles what happens when the Player gets caught
    private void CatchPlayer()
    {
        // Confirm that the catch happened
        Debug.Log("CAUGHT!");

        // Make sure we still have a reference to the Player
        if (player != null)
        {
            // Find the PlayerInventory attached to the Player
            PlayerInventory inventory =
                player.GetComponent<PlayerInventory>();

            // Remove any loot the Player was carrying
            if (inventory != null)
            {
                inventory.LoseCarriedItem();
            }

            // Find the GameManager in the scene
            GameManager gameManager =
                FindAnyObjectByType<GameManager>();

            // Increase the number of times the Player was caught
            if (gameManager != null)
            {
                gameManager.PlayerCaught();
            }

            // Return the Player to the starting location
            if (playerStart != null)
            {
                player.position = playerStart.position;
            }
        }

        // Reset detection
        playerDetected = false;
        playerInsideVision = false;
        detectionTimer = 0f;

        // Clear the stored Player reference
        player = null;
    }
}