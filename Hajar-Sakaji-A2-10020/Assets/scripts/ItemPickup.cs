using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemSO item; // Reference to the Scriptable Object containing item data
    public int amount = 1; // Default amount for this pickup

    // This script only stores item data.
    // The actual pickup action is handled in ObjectPickup.cs when the player interacts with this object.
}
