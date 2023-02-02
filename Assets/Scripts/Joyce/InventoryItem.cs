using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Joyce Mai
public class InventoryItem
{
    public Sweets sweet; // reference to the item data
    public int stackSize { get; private set; }
    public InventoryItem(Sweets _sweet, int numAdded = 1)
    {
        sweet = _sweet;
        AddToStack(numAdded); // add to the stack since an inventory item is being created
    }

    public void AddToStack( int num = 1)
    {
        stackSize += num;
    }

    public void RemoveFromStack(int num = 1)
    {
        stackSize -= num;
    }
}
