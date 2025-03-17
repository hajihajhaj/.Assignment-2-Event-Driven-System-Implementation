using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // Reference to the Inventory system
    public Transform inventoryPanel; // Parent object for inventory slots
    public GameObject inventorySlotPrefab; // Prefab used to display inventory items

    private List<GameObject> slots = new List<GameObject>(); // List to keep track of inventory slot UI elements

    void Start()
    {
        // Find the active Inventory object in the scene
        inventory = FindFirstObjectByType<Inventory>();

        // Ensure this UI object persists between scenes
        DontDestroyOnLoad(this.gameObject);

        // Subscribe to inventory update events
        inventory.InventoryUpdated += UpdateUI;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the inventory event when this object is destroyed
        inventory.InventoryUpdated -= UpdateUI;
    }

    private void UpdateUI()
    {
        Debug.Log("Updating Inventory UI...");

        // Clear old slots safely to avoid modifying the list while iterating
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            Destroy(slots[i]);
        }
        slots.Clear();

        // Populate UI with current inventory items
        foreach (KeyValuePair<ItemSO, int> entry in inventory.inventory)
        {
            Debug.Log($"Updating UI for: {entry.Key.itemName} - {entry.Value}");

            // Create a new UI slot for the inventory item
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);

            // Update the text to show item name and quantity
            newSlot.GetComponentInChildren<TextMeshProUGUI>().text = entry.Key.itemName + " : " + entry.Value;

            // Update the icon to match the inventory item
            newSlot.GetComponentInChildren<Image>().sprite = entry.Key.itemImage;

            // Add the slot to the list for tracking
            slots.Add(newSlot);
        }

        Debug.Log("Inventory UI updated successfully!");
    }
}
