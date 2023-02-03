using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


// Joyce Mai
public class InventorySlot : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private Sprite emptyIcon;
    [SerializeField] private Image x;
    [SerializeField] private TextMeshProUGUI countText;

    private void Start()
    {
        ClearSlot();
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
                
        icon.sprite = item.sweet.icon;
        x.enabled = true;
        countText.enabled = true;
        countText.text = item.stackSize.ToString();
    }
}
