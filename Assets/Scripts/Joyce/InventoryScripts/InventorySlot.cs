using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Joyce Mai
public class InventorySlot : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image icon; // icon to be displayed in the inventory
    [SerializeField] private Image lockedIcon; // Sprite icon overlay displayed when the item has not been unlocked yet
    [SerializeField] private Sprite emptyIcon; // Sprite icon to display when there is nothing currently in the slot
    [SerializeField] private Image x; // reference to the image that displays the card counter symbol
    [SerializeField] private TextMeshProUGUI countText; // reference to the text displaying the count

    private bool unlocked; // bookkeeper variable to track if the slot has been locked or not

    private void Awake()
    {
        unlocked = false;
        LockSlot();
        ClearSlot();
    }

    private void LockSlot()
    {
        unlocked = false;
        lockedIcon.enabled = true;
    }

    private void UnlockSlot()
    {
        lockedIcon.enabled = false;
    }

    public void ClearSlot()
    {
        icon.sprite = emptyIcon;
        x.enabled = false;
        countText.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }
        
        if (!unlocked) // if it's the first time drawing this slot, set to unlocked
        {
            unlocked = true;
            UnlockSlot();
        }

        icon.sprite = item.sweet.icon;
        x.enabled = true;
        countText.enabled = true;
        countText.text = item.stackSize.ToString();
    }
}
