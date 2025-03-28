using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject freeExploreButton;       // Free Explore Button
    public GameObject followInstructionsButton; // Follow Instructions Button
    public GameObject player;                  // Player GameObject
    public Vector3 targetPosition;             // Position to teleport the player

    private bool isExploring = false;          // To track free exploration state

    // Method for Free Explore Mode
    public void OnFreeExplore()
    {
        Debug.Log("Free Explore Mode Activated!");
        
        // Enable free exploration (customize if necessary)
        EnablePlayerControl(true);

        // Hide buttons
        HideButtons();
    }

    // Method for Following Instructions and Teleportation
    public void OnFollowInstructions()
    {
        Debug.Log("Following Instructions...");

        // Teleport the player
        TeleportPlayer(targetPosition);

        // Hide buttons
        HideButtons();
    }

    // Hide both buttons
    private void HideButtons()
    {
        freeExploreButton.SetActive(false);
        followInstructionsButton.SetActive(false);
    }

    // Enable or disable player control based on exploration mode
    private void EnablePlayerControl(bool enable)
    {
        if (player.GetComponent<CharacterController>() != null)
        {
            player.GetComponent<CharacterController>().enabled = enable;
        }
    }

    // Move the player to a new position
    private void TeleportPlayer(Vector3 newPosition)
    {
        player.transform.position = newPosition;
    }
}
