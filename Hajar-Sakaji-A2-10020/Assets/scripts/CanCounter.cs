using UnityEngine;
using TMPro;


//Manages the UI counter for cans in the inventory.
//Updates dynamically when cans are picked up or removed.

public class CanCounter : MonoBehaviour
{
    public TextMeshProUGUI countText;  //Reference to the UI text element that displays the number of cans
    public ItemSO canData;  //The specific "can" item type this counter tracks
    private Inventory inventory;  //Reference to the player's inventory system

    //Initializes the CanCounter, finds the inventory system, and subscribes to inventory updates.
    private void Start()
    {
        //Find the inventory system in the scene
        inventory = FindObjectOfType<Inventory>();

        if (inventory != null)
        {
            Debug.Log("CanCounter found Inventory!");

            //Subscribe to the InventoryUpdated event, so this script updates when inventory changes
            inventory.InventoryUpdated += UpdateCanCount;

            //Update the count immediately when the game starts
            UpdateCanCount();
        }
        else
        {
            Debug.LogWarning("Inventory not found in the scene! CanCounter will not update.");
        }
    }

    //Unsubscribes from the inventory event when this object is destroyed to prevent memory leaks.
    private void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.InventoryUpdated -= UpdateCanCount;
        }
    }

    //Updates the UI text to reflect the current number of cans in the inventory.
    //This function is called whenever the inventory changes.
    private void UpdateCanCount()
    {
        Debug.Log("UpdateCanCount() called!");

        int count = 0;  //Default to 0 cans

        //Ensure both the inventory and canData are set before checking
        if (inventory != null && canData != null)
        {
            if (inventory.inventory.ContainsKey(canData))
            {
                count = inventory.inventory[canData]; //Get the number of cans in inventory
            }
        }

        Debug.Log("Can count in inventory: " + count);

        //Update the UI text if the reference is assigned
        if (countText != null)
        {
            countText.text = count.ToString();
            Debug.Log("Updated UI count to: " + count);
        }
        else
        {
            Debug.LogWarning("countText UI element is NULL! UI will not update.");
        }
    }
}
