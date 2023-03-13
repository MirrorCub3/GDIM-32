using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Joyce Mai
public class PlantingPatch : MonoBehaviour
{
    public enum DirtStates { LOCKED, GROW, SPAWN, WAIT } // making public such that external programs can set the state of the dirt patch once unlocked

    [Header("Card Spawn")]
    [SerializeField] private GameObject spawnPrefab; // will be used to instantiate the card
    // create private references here to the spawn prefabs's details via scritable object data
    [SerializeField] private Sweets spawnData;

    [Header("Visuals and UI")]
    [SerializeField] private Image lockedIcon; // image to display when the patch is not yet usable
    [SerializeField] private GameObject sprout; // reference to the sprout sprite to display during growth process
    [SerializeField] private Slider plantTimeBar; // ui for the slider showing the growth progress
    [SerializeField] private Image fill; // reference to the fill bar image on the progress slider
    [SerializeField] private DirtUISettings colors; // scriptable object which holds the color data for the color bar changes

    [Header("Spawn Data")]
    [SerializeField] private float cooldownMultiplier = 1; // multiplier on the time it takes to cool down
    private float time; // used to set the progress bar fill level

    [Header("State Variables")]
    [SerializeField] private DirtStates currState = DirtStates.GROW; // used to keep track of the current state

    private bool paused;

    void Start()
    {
        plantTimeBar.maxValue = spawnData.growthTime;
        if (currState == DirtStates.SPAWN) 
        {
            Spawn();
        }
        else if (currState == DirtStates.GROW)
        {
            Grow();
        }
        else if (currState == DirtStates.WAIT)
        {
            Cooldown();
        }
        else
        {
            Locked();
        }

    }

    void Update()
    {
        if (paused)
            return;

        if (currState == DirtStates.GROW) // if the current state is grow, increase the time and reflect it on the progress bar
        {
            time += Time.deltaTime;
            if (time >= spawnData.growthTime) // if enough time has passed, spawn the item
            {
                Spawn();
            }
            else
            {
                plantTimeBar.value = time;
            }
        }
        else if (currState == DirtStates.WAIT) // if on cooldown state, then decrement the time
        {
            time -= Time.deltaTime / cooldownMultiplier;
            plantTimeBar.value = time;
            if (time <= 0)
            {
                Grow();
            }
        }
    }

    private void OnEnable()
    {
        GameManager.OnWorldDisable += PauseSelf;
        GameManager.OnWorldEnable += ResumeSelf;
    }

    private void OnDisable()
    {
        GameManager.OnWorldDisable -= PauseSelf;
        GameManager.OnWorldEnable -= ResumeSelf;
    }

    private void PauseSelf()
    {
        paused = true;
    }

    private void ResumeSelf()
    {
        paused = false;
    }

    private void Spawn()
    {
        currState = DirtStates.SPAWN;

        lockedIcon.enabled = false;
        sprout.SetActive(false);
        time = spawnData.growthTime;
        GameObject spawn = Instantiate(spawnPrefab, transform);
        // subscribing to the instance's collect action
        spawn.GetComponent<RecipeCardCollectable>().ThisCardCollectedNotif += OnCollect;
        plantTimeBar.gameObject.SetActive(false);
    }
    
    private void Cooldown()
    {
        currState = DirtStates.WAIT;

        lockedIcon.enabled = false;
        sprout.SetActive(false);
        time = spawnData.growthTime;
        plantTimeBar.value = time;
        plantTimeBar.gameObject.SetActive(true);
        fill.color = colors.WaitCol();
    }

    private void Grow()
    {
        currState = DirtStates.GROW;

        lockedIcon.enabled = false;
        sprout.SetActive(true);
        time = 0;
        plantTimeBar.value = time;
        fill.color = colors.GrowCol();
    }

    private void Locked() // used when the dirt patch is on the map but yet to become useable
    {
        currState = DirtStates.LOCKED;

        sprout.SetActive(false);
        plantTimeBar.gameObject.SetActive(false);
        time = 0;
        lockedIcon.enabled = true;
    }

    private void OnCollect() // called via subscription when the spawned card object is collected
    {
        if (currState == DirtStates.WAIT)
            return;
        Cooldown(); // enter cooldown stage after collected
    }

    public void Unlock(DirtStates state = DirtStates.GROW) // a public method for external code like the store to set the state of dirt once unlocked
    {
        currState = state;
        switch(state)
        {
            case DirtStates.GROW:
                Grow();
                break;
            case DirtStates.WAIT:
                Cooldown();
                break;
            case DirtStates.SPAWN:
                Spawn();
                break;
            default:
                print("You should not be setting to locked state in unlock");
                break;
        }
    }
}
