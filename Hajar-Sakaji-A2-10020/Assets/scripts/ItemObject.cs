using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemSO referenceItem; // The item data associated with this object

    // Handles picking up the item by adding it to the inventory and removing it from the scene.
    public void HandlePickup()
    {
        // Find the inventory system in the scene
        Inventory inventory = FindObjectOfType<Inventory>();

        // Ensure the inventory exists and the item data is valid
        if (inventory != null && referenceItem != null)
        {
            // Add the item to the inventory
            inventory.AddItem(referenceItem, 1);

            // Log the pickup event
            Debug.Log("Picked up: " + referenceItem.itemName);

            // Remove the physical object from the scene after pickup
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Pickup failed: Inventory or reference item is missing!");
        }
    }
}
