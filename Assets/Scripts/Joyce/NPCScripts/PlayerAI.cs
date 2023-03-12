using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joyce Mai
public class PlayerAI : MonoBehaviour
{
    [Header("AI")]
    public Queue<Transform> cardLocs;
    public bool meeting { get; private set;}

    [Header("Strategies")]
    [SerializeField] private Strategy followStrat;
    [SerializeField] private Strategy collectStrat;
    [SerializeField] private Strategy meetStrat;

    private Dictionary<AIStates, Strategy> strats = new Dictionary<AIStates, Strategy>(); 
    private enum AIStates { FOLLOW, COLLECT, MEET}
    private AIStates currState;
    private AIStates prevState;

    private void OnEnable()
    {
        RecipeCardCollectable.OnCardSpawned += OnCardSpawned;  
        // subcribe to player entering event
    }
    private void OnDisable()
    {
        RecipeCardCollectable.OnCardSpawned -= OnCardSpawned;
        // unsubcribe to player entering event
    }

    void Awake()
    {
        cardLocs = new Queue<Transform>();
        meeting = false;

        strats.Add(AIStates.FOLLOW, followStrat);
        strats.Add(AIStates.COLLECT, collectStrat);
        strats.Add(AIStates.MEET, meetStrat);

        foreach (KeyValuePair<AIStates, Strategy> pair in strats)
            pair.Value.enabled = false;

        currState = AIStates.FOLLOW;
        prevState = currState;
        strats[currState].enabled = true;
    }

    void Update()
    {
        // if the player is entering, go to them
        // else, if there are cards to collect, collect them
        // if nothing to do, follow
        if (meeting)
            currState = AIStates.MEET;
        else if (cardLocs.Count > 0)
            currState = AIStates.COLLECT;
        else
            currState = AIStates.FOLLOW;

        if (prevState != currState) // if switching states, disable the previous
        {
            strats[prevState].enabled = false;
            prevState = currState;
        }

        strats[currState].enabled = true;
    }

    void OnCardSpawned(Transform cardLoc)
    {
        if (!cardLoc)
            return;
        cardLocs.Enqueue(cardLoc);
    }

    void PlayerInteracting(bool interacting) // attach this to the player event
    {
        meeting = interacting;
    }
}
