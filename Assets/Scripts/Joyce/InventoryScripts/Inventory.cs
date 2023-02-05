using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private List<InventoryItem> inventory = new List<InventoryItem>(); // a list of the current inventory items
    private Dictionary<Sweets, InventoryItem> itemDictionary = new Dictionary<Sweets, InventoryItem>(); // a dictionary that associates each sweet's data with an invemtory item

    [Header("Manager")]
    [SerializeField] private List<Sweets> sweetsOrder; // sets the order for the sweets that will display in the inventory
    [SerializeField] private List<InventorySlot> slotOrder; // list of references of the inventory slots in order
    private Dictionary<Sweets, InventorySlot> inventorySlots = new Dictionary<Sweets, InventorySlot>(); // dictionary that associates each sweet's data with an inventory slot

    private void Awake()
    {
        // at the start, zip the sweets list and slot order list together
        for( int i = 0; i < slotOrder.Count; i++)
        {
            inventorySlots.Add(sweetsOrder[i], slotOrder[i]);
        }

        inventoryData.Init();
    }

    private void OnEnable()
    {
        // subscribing to be notified any time a card is collected
        RecipeCardCollectable.OnCardCollectedGlobal += Add;
        ResetInventory();
        MatchToData();
        DrawInventory();
    }

    private void OnDisable()
    {
        // unsubscribing to be notified any time a card is collected
        RecipeCardCollectable.OnCardCollectedGlobal -= Add;
    }

    private void MatchToData() // matches held data to data in inventoryData
    {
        if (inventoryData.itemDictionary == null) // only set if the dictionary is not empty
            return;
        foreach (KeyValuePair<Sweets, InventoryItem> item in inventoryData.itemDictionary)
        {
            InventoryItem newItem = new InventoryItem(item.Value.sweet, item.Value.stackSize); // making a copy of the inventory item data held in inventoryData
            inventory.Add(newItem);
            itemDictionary.Add(item.Key, newItem); // adding it to the dictionary
        }
    }

    private void ResetInventory() // resets the entire inventory
    {
        foreach(Sweets slotKey in inventorySlots.Keys)
        {
            inventorySlots[slotKey].ClearSlot();
        }
    }

    private void DrawInventory() // redraws the entire inventory to match saved data
    {
        foreach(KeyValuePair<Sweets, InventoryItem> item in itemDictionary)
        {
            DrawInventorySlot(item.Key, item.Value);
        }
    }

    // updates the specific slot visuals associated with the sweet updated
    private void DrawInventorySlot(Sweets sweetUpdated, InventoryItem item)
    {
        inventorySlots[sweetUpdated].ClearSlot();
        inventorySlots[sweetUpdated].DrawSlot(item);
    }

    public void Add(Sweets sweet, int count)
    {
        if (itemDictionary.TryGetValue(sweet, out InventoryItem item)) // if an item of this type is already in the inventory
        {
            item.AddToStack(count); // increment the stack by the amount collected
            DrawInventorySlot(sweet, item); // update the UI
            inventoryData.Add(sweet, count);
        }
        else // if the item is new to the inventory
        {
            InventoryItem newItem = new InventoryItem(sweet, count); // create a new inventory item to associate with it
            inventory.Add(newItem); // add the item to the inventory list
            itemDictionary.Add(sweet, newItem); // add the item to the inventory dictionary with the sweets data as the key
            DrawInventorySlot(sweet, newItem); // update the UI
            inventoryData.Add(sweet, count);
        }
    }
    public void Remove(Sweets sweet, int count)
    {
        if (itemDictionary.TryGetValue(sweet, out InventoryItem item)) // make sure the item is one already in the inventory
        {
            item.RemoveFromStack(count); // remove the amount from the item's count
            if (item.stackSize <= 0) // if there is no more of that object left
            {
                inventory.Remove(item); // remove the item from inventory
                itemDictionary.Remove(sweet); // remove the sweet from the dictionary
            }
            DrawInventorySlot(sweet, item); // update UI
            inventoryData.Remove(sweet, count);
        }
    }

}
