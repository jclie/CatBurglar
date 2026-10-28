using UnityEngine;

// Controls the Homeowner's automatic patrol movement
public class HomeownerPatrol : MonoBehaviour
{
    // Controls how fast the Homeowner moves
    public float moveSpeed = 2f;

    // Stores the locations the Homeowner will patrol between
    public Transform[] patrolPoints;

    // Reference to the Vision child object
    // This allows us to rotate the field of vision
    public Transform vision;

    // Keeps track of which patrol point
    // the Homeowner is currently moving toward
    private int currentPointIndex = 0;

    // Update runs once every frame
    void Update()
    {
        // Stop if no patrol points have been assigned
        if (patrolPoints.Length == 0)
        {
            return;
        }

        // Get the current patrol destination
        Transform targetPoint = patrolPoints[currentPointIndex];

        // Calculate the direction from the Homeowner
        // toward the patrol point
        Vector2 direction =
            targetPoint.position - transform.position;

        // Move the Homeowner toward the patrol point
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime
        );

        // Turn the field of vision toward
        // the direction the Homeowner is walking
        UpdateVisionDirection(direction);

        // Check whether the Homeowner reached
        // the current patrol point
        if (Vector2.Distance(
                transform.position,
                targetPoint.position
            ) < 0.1f)
        {
            // Move on to the next patrol point
            currentPointIndex++;

            // If we reached the final patrol point,
            // start again at the first point
            if (currentPointIndex >= patrolPoints.Length)
            {
                currentPointIndex = 0;
            }
        }
    }

    // Rotates the Vision object so it faces
    // the direction the Homeowner is walking
    private void UpdateVisionDirection(Vector2 direction)
    {
        // Make sure a Vision object was assigned
        if (vision == null)
        {
            return;
        }

        // Calculate the angle of the movement direction
        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        // Rotate the Vision child object
        vision.rotation = Quaternion.Euler(
            0f,
            0f,
            angle - 180f
        );
    }
}