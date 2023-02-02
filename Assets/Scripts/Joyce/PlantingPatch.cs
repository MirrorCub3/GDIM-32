using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Joyce Mai
public class PlantingPatch : MonoBehaviour
{
    [Header("Card Spawn")]
    [SerializeField] private GameObject spawnPrefab; // will be used to instantiate the card
    // create private references here to the spawn prefabs's details via scritable object data
    //[SerializeField] private [ScriptableObjectTypeHere] spawnData;

    [Header("UI")]
    [SerializeField] private Slider plantTimeBar; // ui for the slider showing the growth progress

    [Header("Spawn Data")]
    [SerializeField] private float plantTime; // the time it takes for the item to finish growing
    [SerializeField] private float cooldown;
    private float time;

    private enum DirtStates { WAIT, GROW, SPAWN }
    private DirtStates currState = DirtStates.GROW;
    private bool spawnExists; // will be used to determine wheter or not to increment time
    // private references here
    void Start()
    {
        if (currState == DirtStates.SPAWN) 
        {
            Instantiate(spawnPrefab, this.transform);
            spawnExists = true;
            plantTimeBar.enabled = false;
        }
        else if (currState == DirtStates.GROW)
        {
            time = 0;
            spawnExists = false;
            plantTimeBar.enabled = true;
            plantTimeBar.value = time;
            plantTimeBar.maxValue = plantTime;
        }
        else
        {
            //print();
            // coroutine??
        }

    }

    void Update()
    {
        if (currState == DirtStates.GROW)
        {
            time += Time.deltaTime;
            if (time >= plantTime)
            {
                // disable ui
                // switch state to spawn
                // reset time bookkeeping vars
            }
            else
            {
                plantTimeBar.value = time;
            }
        }
    }

    private void OnCollect()
    {
        // call this via subscribe event for when it's child object becomes collected
    }
}
