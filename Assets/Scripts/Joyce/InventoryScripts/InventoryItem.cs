using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// Joyce Mai
// a class for a stackable inventory item
public class InventoryItem 
{
    public Sweets sweet; // reference to the item data
    public int stackSize { get; private set; } // stack size can be referenced outside of the class but it must be set in the class
    public InventoryItem(Sweets _sweet, int numAdded = 1) // constructor for the inventory item
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
