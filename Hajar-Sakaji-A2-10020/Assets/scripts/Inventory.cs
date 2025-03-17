using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    // Dictionary to store items in inventory, with their quantity
    public Dictionary<ItemSO, int> inventory = new Dictionary<ItemSO, int>();
    public int maxInventorySlots = 50; // Maximum inventory slots

    // Event triggered when inventory changes (for UI updates or gameplay effects)
    public delegate void OnInventoryChanged();
    public event OnInventoryChanged InventoryUpdated;

    private void Start()
    {
        Debug.Log("Inventory.cs is running!"); // Debug to confirm script is active

        // Ensures the inventory persists across scene changes
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddItem(ItemSO newItem, int amount)
    {
        Debug.Log("Trying to add item: " + newItem.itemName + " Amount: " + amount);

        // If the item is already in the inventory, increase its count
        if (inventory.ContainsKey(newItem))
        {
            inventory[newItem] += amount;
        }
        else
        {
            // Add new item to inventory
            inventory.Add(newItem, amount);
        }

        Debug.Log("Added " + amount + " " + newItem.itemName + "(s). Total now: " + inventory[newItem]);

        // Notify any listeners (UI, game logic) that the inventory has changed
        InventoryUpdated?.Invoke();
    }

    public void RemoveItem(ItemSO itemToRemove, int amount)
    {
        if (inventory.ContainsKey(itemToRemove))
        {
            Debug.Log("Removing " + amount + " " + itemToRemove.itemName + "(s). Current count: " + inventory[itemToRemove]);

            inventory[itemToRemove] -= amount;

            // If the item count reaches zero or below, remove it completely
            if (inventory[itemToRemove] <= 0)
            {
                Debug.Log("Item removed completely: " + itemToRemove.itemName);
                inventory.Remove(itemToRemove);
            }

            Debug.Log("New count after removal: " + (inventory.ContainsKey(itemToRemove) ? inventory[itemToRemove] : 0));

            // Notify any listeners that the inventory has changed
            InventoryUpdated?.Invoke();
        }
        else
        {
            Debug.LogWarning("Tried to remove " + itemToRemove.itemName + " but it wasn't in the inventory!");
        }
    }

    // Checks if the inventory contains a specific item in the required amount
    public bool HasItem(ItemSO item, int amount)
    {
        return inventory.ContainsKey(item) && inventory[item] >= amount;
    }
}
