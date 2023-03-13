using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joyce Mai
public class PlayerAI : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private PlayerInteract player1; // will give alert when entering restaurant
    [HideInInspector] public Queue<Transform> cardLocs; // locations of cards queued up, will be visited in first-in first-out order
    private bool meeting; // used to track when player 1 is entering a restaurant

    [Header("Strategies")]
    [SerializeField] private Strategy followStrat; // strategy to activate when following
    [SerializeField] private Strategy collectStrat; // strategy to activate when collecting
    [SerializeField] private Strategy meetStrat; // strategy to activate when meeting player 1

    private Dictionary<AIStates, Strategy> strats = new Dictionary<AIStates, Strategy>(); // dictionary of strategies indexed by state
    private enum AIStates { FOLLOW, COLLECT, MEET}
    private AIStates currState; // used to track current state
    private AIStates prevState; // used to cmpare against current state

    private void OnEnable()
    {
        RecipeCardCollectable.OnCardSpawned += OnCardSpawned; // subscribing to card spawn event
        player1.PlayerEntering += PlayerInteracting; // subscribing to player entering event
    }
    private void OnDisable()
    {
        RecipeCardCollectable.OnCardSpawned -= OnCardSpawned; // unsubscribing to card spawn event
        player1.PlayerEntering += PlayerInteracting; // unsubscribing to player entering event
    }

    void Awake()
    {
        cardLocs = new Queue<Transform>();
        meeting = false;

        strats.Add(AIStates.FOLLOW, followStrat);
        strats.Add(AIStates.COLLECT, collectStrat);
        strats.Add(AIStates.MEET, meetStrat);

        // set each state to false to start with
        foreach (KeyValuePair<AIStates, Strategy> pair in strats)
            pair.Value.enabled = false;

        // follow is default state
        currState = AIStates.FOLLOW;
        prevState = currState;
        strats[currState].enabled = true;
    }

    void Update()
    {
        if (meeting) // if the player is entering a kitchen, go to them
            currState = AIStates.MEET;
        else if (cardLocs.Count > 0) // if there are cards to collect, collect
            currState = AIStates.COLLECT;
        else // if theres nothing to do, follow
            currState = AIStates.FOLLOW;

        if (prevState != currState) // if switching states, disable the previous
        {
            strats[prevState].enabled = false;
            prevState = currState;
        }

        strats[currState].enabled = true;
    }

    void OnCardSpawned(Transform cardLoc) // add a new location onto list
    {
        if (!cardLoc)
            return;
        cardLocs.Enqueue(cardLoc);
    }

    void PlayerInteracting(bool interacting) // sets if player 1 is entering a restaurant
    {
        meeting = interacting;
    }
}
