using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Joyce Mai
public class RecipeCardCollectable : MonoBehaviour, ICollectable
{
    // subscribers to this event will be notified for every card collected
    public delegate void HandleCardCollectedArgs(Sweets sweet, int count);
    public static event HandleCardCollectedArgs OnCardCollectedGlobal; // will be triggered for all cards

    // subscribers to this event will be notified when a new card is spawned and it's location
    public delegate void CardSpawned(Transform loc);
    public static event CardSpawned OnCardSpawned; // will be triggered for all cards

    // subscribers to this event will only be notified for specific card isntance
    public delegate void HandleCardCollected();
    public event HandleCardCollected ThisCardCollectedNotif;

    [Header("Sweets SO")]
    [SerializeField] private Sweets sweet;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TextMeshProUGUI countText;

    [Header("ItemData")]
    [SerializeField] private int itemCount;

    void Awake()
    {
        // setting the visuals
        sr.sprite = sweet.icon;
        countText.text = itemCount.ToString();
    }

    private void Start()
    {
        if (OnCardSpawned != null)
            OnCardSpawned.Invoke(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        Destroy(gameObject);

        // when collected, alert subscribers and pass along information
        if (OnCardCollectedGlobal != null)
            OnCardCollectedGlobal.Invoke(sweet, itemCount);

        // only alert if there are subscribers
        if (ThisCardCollectedNotif != null)
            ThisCardCollectedNotif.Invoke();
    }
}
