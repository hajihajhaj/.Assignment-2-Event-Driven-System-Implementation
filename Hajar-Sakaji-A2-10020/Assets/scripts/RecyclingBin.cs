using UnityEngine;

public class RecyclingBin : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager for tracking recycled cans

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the recycling bin is a pickup item (a can)
        if (other.CompareTag("Pickup")) // Ensure cans are tagged as "Pickup"
        {
            Destroy(other.gameObject); // Remove the can from the scene
            gameManager.CanRecycled(); // Notify the GameManager that a can was recycled
        }
    }
}
