using UnityEngine;

// Manages the sound effects used during gameplay
public class AudioManager : MonoBehaviour
{
    // AudioSource used to play sound effects
    private AudioSource audioSource;

    // Sound played when loot is picked up
    public AudioClip lootPickupSound;

    // Sound played when loot is deposited
    public AudioClip lootDepositSound;

    // Sound played when the Homeowner detects the Player
    public AudioClip alertSound;

    // Sound played when the Player gets caught
    public AudioClip timerRunningLowSound;

    // Sound played when the Player hides
    public AudioClip hideSound;

    // Sound played when the game ends
    public AudioClip gameOverSound;

    // Get the AudioSource when the game starts
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Plays the loot pickup sound
    public void PlayLootPickup()
    {
        audioSource.PlayOneShot(lootPickupSound);
    }

    // Plays the loot deposit sound
    public void PlayLootDeposit()
    {
        audioSource.PlayOneShot(lootDepositSound);
    }

    // Plays the homeowner alert sound
    public void PlayAlert()
    {
        audioSource.PlayOneShot(alertSound);
    }

    // Plays the caught sound
    public void PlayTimerRunningLow()
    {
        audioSource.PlayOneShot(timerRunningLowSound);
    }

    // Plays the hiding sound
    public void PlayHide()
    {
        audioSource.PlayOneShot(hideSound);
    }

    // Plays the game over sound
    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    // Stops any sound currently playing through the AudioSource
    public void StopSound()
    {
        // Make sure the AudioSource exists before stopping it
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}