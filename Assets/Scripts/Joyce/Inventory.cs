using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<Sweets, InventoryItem> itemDictionary = new Dictionary<Sweets, InventoryItem>();

    private void OnEnable()
    {
        RecipeCardCollectable.OnCardCollectedGlobal += Add;
    }

    private void OnDisable()
    {
        RecipeCardCollectable.OnCardCollectedGlobal -= Add;
    }

    public void Add(Sweets sweet, int count) // add another parameter here for incrementing stack by a certain cout
    {
        if (itemDictionary.TryGetValue(sweet, out InventoryItem item))
        {
            item.AddToStack(count);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(sweet, count);
            inventory.Add(newItem);
            itemDictionary.Add(sweet, newItem);
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
        }
    }

}
