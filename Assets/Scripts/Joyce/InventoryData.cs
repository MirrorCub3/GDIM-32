using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
// stores the inventory data between scenes
[CreateAssetMenu(menuName = "PersistentData/ InventoryData")]
public class InventoryData : ScriptableObject, IReset
{
    public List<InventoryItem> inventory { get; private set;}
    public Dictionary<Sweets, InventoryItem> itemDictionary { get; private set; }

    public void Init()
    {
        itemDictionary = new Dictionary<Sweets, InventoryItem>();

        if(inventory == null)
        {
            inventory = new List<InventoryItem>();
            return;
        }

        foreach(InventoryItem item in inventory)
        {
            itemDictionary.Add(item.sweet, item);
        }
    }

    public void Add(Sweets sweet, int count)
    {
        if (itemDictionary.TryGetValue(sweet, out InventoryItem item)) // if an item of this type is already in the inventory
        {
            item.AddToStack(count); // increment the stack by the amount collected
        }
        else // if the item is new to the inventory
        {
            InventoryItem newItem = new InventoryItem(sweet, count); // create a new inventory item to associate with it
            inventory.Add(newItem); // add the item to the inventory list
            itemDictionary.Add(sweet, newItem); // add the item to the inventory dictionary with the sweets data as the key
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
        }
    }

    public void Reset()
    {
        inventory.Clear();
        itemDictionary.Clear();
    }
}
