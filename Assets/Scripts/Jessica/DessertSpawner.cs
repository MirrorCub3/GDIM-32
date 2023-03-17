using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jessica Lam
// Also the Timing Controller
// Has the list of Dessert Qualitys
public class DessertSpawner : MonoBehaviour
{  
    // Controls timing based on the song BPM
    [SerializeField] Animator P1Anim;
    [SerializeField] Animator P2Anim;
    [SerializeField] Animator DessertToP1;
    [SerializeField] Animator ConveyerBelt;
    [SerializeField] Animator ConveyerBeltBack;

    [SerializeField] float idleTime; // time to wait before starting song
    [SerializeField] float BPM; // BPM of the song
    float startTime;
    bool started;

    // Spawn Dessert
    float Timer;
    [SerializeField] private  DessertController dessertPrefab;
    DessertController dessertClone;

    // List of Dessert
    public List<DessertController> desserts;

    // Take Kitchen Manager Script
    [SerializeField] private KitchenManager kitchenManager;

    // Used for different timing & perks for each kitchen
    int dessertCount;
    [SerializeField] Sweets sweet;

    // Cookie kitchen video and perks
    [SerializeField] GameObject video;

    void Start()
    {
        if (video)
        {
            video.SetActive(false);
        }
        desserts = new List<DessertController>();
        Timer = .3f;
        P1Anim.speed = (BPM/60f);
        P2Anim.speed = (BPM/60f);
        ConveyerBelt.speed = (BPM / 60f);
        ConveyerBeltBack.speed = (BPM / 60f);
        DessertToP1.speed = (BPM / 60f);
        startTime = 0f;
        dessertCount = 0;
    }

    void WaitFor(Animator anim)
    {
        anim.SetBool("WaitFor", true);
        if (startTime >= idleTime){
            anim.SetBool("WaitFor", false);
            started = true;
        }
    }

    public void AddDessert(DessertController dessert)
    {
        desserts.Add(dessert);
    }

    void Update()
    {   // start Timer
        if (started == false){
            startTime = startTime + Time.deltaTime;
            WaitFor(P1Anim);
            WaitFor(P2Anim);
            WaitFor(DessertToP1);
            WaitFor(ConveyerBelt);
            WaitFor(ConveyerBeltBack);
        }
        else {
            // Creates a new dessert every 2 seconds
            Timer -= Time.deltaTime;
            if (Timer <= 0f)
            {
                Debug.Log(dessertCount);
                dessertCount += 1;
                dessertClone = Instantiate(dessertPrefab);
                Animator dessertAnim = dessertClone.GetComponent<Animator>();
                dessertAnim.speed = (BPM/60f);
                desserts.Add(dessertClone);

                if (kitchenManager){ // testing purposes
                    kitchenManager.ReduceDessertByOne(); // call function from kitchen manager script to reduce dessert by one
                }
                Timer = (60f/BPM)*2f;
            }
        }
        if (dessertCount == 39 && sweet.sweetName == "Cookie")
        {
            video.SetActive(true);
        }
        if (dessertCount == 0 && sweet.sweetName == "Souffle")
        {
            video.SetActive(true);
        }
    }

    public void DestroyAllDesserts() // destroys all the prefabs when exiting kitchen
    {
        foreach (DessertController dessert in desserts)
            {
                Destroy(dessert.gameObject);
            }
    }
}