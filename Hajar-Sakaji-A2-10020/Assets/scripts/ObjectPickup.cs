using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPickup : MonoBehaviour
{
    public Camera cam; // Reference to the player's camera
    public float pickupRange = 3f; // The maximum range at which objects can be picked up
    public TextMeshProUGUI pickupText; // UI text that displays pickup instructions
    public TextMeshProUGUI inventoryCountText; // UI text that displays the number of picked-up items

    private List<GameObject> inventory = new List<GameObject>(); // List storing picked-up objects
    private GameObject currentItem; // The object the player is currently looking at
    private bool canPickup = false; // Determines if an object is in range and can be picked up

    void Update()
    {
        // Check if the player is looking at a pickup object within range
        if (Physics.SphereCast(cam.transform.position, 0.5f, cam.transform.forward, out RaycastHit hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                currentItem = hit.collider.gameObject;
                pickupText.gameObject.SetActive(true); // Show pickup instruction UI
                canPickup = true;
            }
            else
            {
                pickupText.gameObject.SetActive(false);
                canPickup = false;
            }
        }
        else
        {
            pickupText.gameObject.SetActive(false);
            canPickup = false;
        }

        // Left-click to pick up an object
        if (Input.GetMouseButtonDown(0) && canPickup && currentItem != null)
        {
            PickupObject(currentItem);
        }

        // Right-click to drop the last picked-up object
        if (Input.GetMouseButtonDown(1) && inventory.Count > 0)
        {
            DropObject();
        }
    }

    void PickupObject(GameObject obj)
    {
        Debug.Log("Picking up: " + obj.name);

        // Add the object to the inventory if it's not already in it
        if (!inventory.Contains(obj))
        {
            inventory.Add(obj);
            obj.SetActive(false); // Hide the object from the scene
            UpdateUI(); // Update the inventory UI
        }
        else
        {
            Debug.LogWarning("Object already in inventory!");
        }
    }

    void DropObject()
    {
        Debug.Log("DropObject() called! Inventory count: " + inventory.Count);

        if (inventory.Count == 0)
        {
            Debug.LogWarning("Tried to drop but inventory is EMPTY!");
            return;
        }

        // Get the last picked-up object from the inventory
        GameObject droppedObject = inventory[inventory.Count - 1];
        inventory.RemoveAt(inventory.Count - 1);

        if (droppedObject == null)
        {
            Debug.LogError("No object found to drop!");
            return;
        }

        // Position the object in front of the player when dropped
        droppedObject.transform.position = cam.transform.position + cam.transform.forward * 1.5f;
        droppedObject.SetActive(true);

        Debug.Log("Dropped: " + droppedObject.name + " at " + droppedObject.transform.position);

        // Ensure the dropped object falls naturally
        Rigidbody rb = droppedObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = droppedObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity = Vector3.zero; // Reset velocity

        // Update the inventory UI after dropping an item
        UpdateUI();
    }

    void UpdateUI()
    {
        int count = inventory.Count;
        Debug.Log("UI Updated: " + count);

        // Update the inventory count UI text if assigned in the Inspector
        if (inventoryCountText != null)
        {
            inventoryCountText.text = count.ToString();
        }
        else
        {
            Debug.LogError("UI text reference is missing! Assign inventoryCountText in the Inspector.");
        }
    }
}
