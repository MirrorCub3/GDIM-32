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

    [Header("Kitchen Stuff")]
    // Cookie kitchen video and perks
    [SerializeField] GameObject video;

    // Icecream kitchen perks
    [SerializeField] GameObject darkness;
    [SerializeField] GameObject darknessP1;
    [SerializeField] GameObject darknessP2;

    // Cake kitchen perks
    [SerializeField] GameObject bubble;
    [SerializeField] GameObject bubble2;
    [SerializeField] SpriteRenderer thought;
    [SerializeField] SpriteRenderer thought2;
    [SerializeField] List<Sprite> thoughtList;
    bool justChanged;

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
        if (DessertToP1)
        {
            DessertToP1.speed = (BPM / 60f);
        }
        startTime = 0f;
        dessertCount = 0;
        if (bubble && bubble2)
        {
            bubble.SetActive(false);
            bubble2.SetActive(false);
            justChanged = false;
        }
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

    public void DestroyAllDesserts() // destroys all the prefabs when exiting kitchen
    {
        foreach (DessertController dessert in desserts)
        {
            Destroy(dessert.gameObject);
        }
    }

    void Update()
    {   // start Timer
        if (started == false){
            startTime = startTime + Time.deltaTime;
            WaitFor(P1Anim);
            WaitFor(P2Anim);
            if (DessertToP1)
                WaitFor(DessertToP1);
            WaitFor(ConveyerBelt);
            WaitFor(ConveyerBeltBack);
        }
        else {
            // Creates a new dessert every 2 seconds
            Timer -= Time.deltaTime;
            if (sweet.sweetName == "Cake")
            {
                if (Timer <= (60f / BPM) && dessertCount >=1 && justChanged == false)
                {
                    cakeControl(true);
                }
            }
            if (Timer <= 0f)
            {
                if (sweet.sweetName == "Cake")
                {
                    cakeControl(false);
                }
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
        if (sweet.sweetName == "Ice Cream")
        {
            if (dessertCount == 0)
            {
                darkness.SetActive(true);
            }
            else
            {
                darkControl();
            }
        }
    }

    void darkControl()
    {
        int remainder = dessertCount % 2;
        if (remainder == 0)
        {
            darknessP1.SetActive(true);
            darknessP2.SetActive(false);
        }
        else if (remainder == 1)
        {
            darkness.SetActive(false);
            darknessP1.SetActive(false);
            darknessP2.SetActive(true);
        }
    }

    void cakeControl(bool p2turn)
    {
        if (p2turn == false)
        {
            int randomNumber = Random.Range(0, thoughtList.Count);
            thought.sprite = thoughtList[randomNumber];
            bubble.SetActive(true);
            bubble2.SetActive(false);
            justChanged = false;
        }
        else if (p2turn == true)
        {
            int randomNumber = Random.Range(0, thoughtList.Count);
            thought2.sprite = thoughtList[randomNumber];
            bubble.SetActive(false);
            bubble2.SetActive(true);
            justChanged = true;
        }
    }
}