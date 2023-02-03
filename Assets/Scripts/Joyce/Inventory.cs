using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<Sweets, InventoryItem> itemDictionary = new Dictionary<Sweets, InventoryItem>();

    [Header("Manager")]
    [SerializeField] private List<Sweets> sweetsOrder;
    [SerializeField] private List<InventorySlot> slotOrder;
    private Dictionary<Sweets, InventorySlot> inventorySlots = new Dictionary<Sweets, InventorySlot>();

    private void Awake()
    {
        // at the start, zip the sweets list and slot order list together
        for( int i = 0; i < slotOrder.Count; i++)
        {
            inventorySlots.Add(sweetsOrder[i], slotOrder[i]);
        }
    }

    private void OnEnable()
    {
        RecipeCardCollectable.OnCardCollectedGlobal += Add;
    }

    private void OnDisable()
    {
        RecipeCardCollectable.OnCardCollectedGlobal -= Add;
    }

    private void ResetInventory()
    {
        foreach(Sweets slotKey in inventorySlots.Keys)
        {
            inventorySlots[slotKey].ClearSlot();
        }
    }

    private void DrawInventorySlot(Sweets sweetUpdated, InventoryItem item)
    {
        inventorySlots[sweetUpdated].ClearSlot();
        inventorySlots[sweetUpdated].DrawSlot(item);
    }

    public void Add(Sweets sweet, int count)
    {
        if (itemDictionary.TryGetValue(sweet, out InventoryItem item))
        {
            item.AddToStack(count);
            DrawInventorySlot(sweet, item);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(sweet, count);
            inventory.Add(newItem);
            itemDictionary.Add(sweet, newItem);
            DrawInventorySlot(sweet, newItem);
        }
    }
    public void Remove(Sweets sweet, int count)
    {
        if (itemDictionary.TryGetValue(sweet, out InventoryItem item))
        {
            item.RemoveFromStack(count);
            if (item.stackSize <= 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(sweet);
            }
            DrawInventorySlot(sweet, item);
        }
    }

}
