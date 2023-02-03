using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Joyce Mai
public class PlantingPatch : MonoBehaviour
{
    private enum DirtStates { WAIT, GROW, SPAWN}

    [Header("Card Spawn")]
    [SerializeField] private GameObject spawnPrefab; // will be used to instantiate the card
    // create private references here to the spawn prefabs's details via scritable object data
    [SerializeField] private Sweets spawnData;

    [Header("Visuals and UI")]
    [SerializeField] private GameObject sprout;
    [SerializeField] private Slider plantTimeBar; // ui for the slider showing the growth progress
    [SerializeField] private Image fill;
    [SerializeField] private DirtUISettings colors;

    [Header("Spawn Data")]
    [SerializeField] private float cooldownMultiplier = 1; // multiplier on the time it takes to cool down
    private float time;

    [Header("State Variables")]
    [SerializeField] private DirtStates currState = DirtStates.GROW;

    void Start()
    {
        plantTimeBar.maxValue = spawnData.PlantGrowthTime;
        if (currState == DirtStates.SPAWN) 
        {
            Spawn();
        }
        else if (currState == DirtStates.GROW)
        {
            Grow();
        }
        else
        {
            StartCoroutine(Cooldown());
        }

    }

    void Update()
    {
        if (currState == DirtStates.GROW)
        {
            time += Time.deltaTime;
            if (time >= spawnData.PlantGrowthTime)
            {
                Spawn();
            }
            else
            {
                plantTimeBar.value = time;
            }
        }
        else if (currState == DirtStates.WAIT)
        {
            time -= Time.deltaTime / cooldownMultiplier;
            plantTimeBar.value = time;
        }
    }

    private void Spawn()
    {
        currState = DirtStates.SPAWN;
        sprout.SetActive(false);
        time = spawnData.PlantGrowthTime;
        GameObject spawn = Instantiate(spawnPrefab, transform);
        // subscribing to the instance's collect action
        spawn.GetComponent<RecipeCardCollectable>().ThisCardCollectedNotif += OnCollect;
        plantTimeBar.gameObject.SetActive(false);
    }

    private IEnumerator Cooldown()
    {
        currState = DirtStates.WAIT;
        sprout.SetActive(false);
        time = spawnData.PlantGrowthTime;
        plantTimeBar.value = time;
        plantTimeBar.gameObject.SetActive(true);
        fill.color = colors.waitCol;

        yield return new WaitForSeconds(spawnData.PlantGrowthTime * cooldownMultiplier);

        Grow();
    }

    private void Grow()
    {
        sprout.SetActive(true);
        currState = DirtStates.GROW;
        time = 0;
        plantTimeBar.value = time;
        fill.color = colors.growCol;
    }

    private void OnCollect()
    {
        if (currState == DirtStates.WAIT)
            return;

        StopAllCoroutines();
        StartCoroutine(Cooldown());
    }
}
