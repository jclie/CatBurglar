using UnityEngine;
using UnityEngine.InputSystem;

// Controls the player's movement
public class PlayerMovement : MonoBehaviour
{
    // Controls how fast the player moves
    // This value can be changed in the Unity Inspector
    public float moveSpeed = 5f;

    // Stores the Rigidbody2D attached to the Player
    private Rigidbody2D rb;

    // Stores the direction the player is moving
    private Vector2 movement;

    // Start runs once when the game begins
    void Start()
    {
        // Gets the Rigidbody2D component attached to the Player
        rb = GetComponent<Rigidbody2D>();
    }

    // Update runs once every frame
    // We use it to check which movement keys are being pressed
    void Update()
    {
        // Reset movement before checking for new input
        movement = Vector2.zero;

        // Move up when W is pressed
        if (Keyboard.current.wKey.isPressed)
        {
            movement.y += 1;
        }

        // Move down when S is pressed
        if (Keyboard.current.sKey.isPressed)
        {
            movement.y -= 1;
        }

        // Move left when A is pressed
        if (Keyboard.current.aKey.isPressed)
        {
            movement.x -= 1;
        }

        // Move right when D is pressed
        if (Keyboard.current.dKey.isPressed)
        {
            movement.x += 1;
        }

        // Keeps diagonal movement from being faster
        // than horizontal or vertical movement
        movement = movement.normalized;
    }

    // FixedUpdate runs at a fixed interval
    // Physics-based movement should be handled here
    void FixedUpdate()
    {
        // Moves the player's Rigidbody2D
        rb.MovePosition(
            rb.position + movement * moveSpeed * Time.fixedDeltaTime
        );
    }
}