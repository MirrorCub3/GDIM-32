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
    [SerializeField] List<InventorySlot> inventorySlots;

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
        foreach(InventorySlot slot in inventorySlots)
        {
            slot.ClearSlot();
        }
    }

    private void DrawInventory()
    {
        ResetInventory();

        for (int i = 0; i < inventory.Capacity; i++)
        {
            inventorySlots[i].ClearSlot();
            if (i < inventory.Count)
                inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    public void Add(Sweets sweet, int count) // add another parameter here for incrementing stack by a certain cout
    {
        if (itemDictionary.TryGetValue(sweet, out InventoryItem item))
        {
            item.AddToStack(count);
            DrawInventory();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(sweet, count);
            inventory.Add(newItem);
            itemDictionary.Add(sweet, newItem);
            DrawInventory();
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
            DrawInventory();
        }
    }

}
