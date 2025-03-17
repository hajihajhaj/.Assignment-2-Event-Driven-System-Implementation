using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Sprite itemImage; // The visual representation of the item
    public string itemName; // The name of the item
    public string itemDescription; // A short description of the item
    public int maxStxkSize = 100; // Maximum amount of this item that can be stacked in inventory
    public bool isConsumable; // Determines if the item can be used/consumed
}
