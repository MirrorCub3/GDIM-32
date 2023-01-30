using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class RecipeCardCollectable : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sr;
    //  will be replaced by reference to the dessert scriptable object
    [SerializeField] private Sprite icon;

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
