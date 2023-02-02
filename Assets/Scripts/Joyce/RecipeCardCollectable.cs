using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Joyce Mai
public class RecipeCardCollectable : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sr;
    //  will be replaced by reference to the dessert scriptable object
    [SerializeField] private Sprite icon;
    [SerializeField] private TextMeshProUGUI countText;

    [Header("ItemData")]
    [SerializeField] private int itemCount;

    void Awake()
    {
        sr.sprite = icon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            // collect the object
            // increment it to the appropriate recipe card in inventory
            Destroy(gameObject);
        }
    }
}
