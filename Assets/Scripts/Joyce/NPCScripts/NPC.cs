using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Joyce Mai and Naman Khurana
public class NPC : MonoBehaviour
{
    public enum States { Wander, Target, Eat, Sleep , Inspect };

    [Header("State Controls")]
    [SerializeField] protected NPC.States currState; // devs can set this in the editor to change the starting state of the NPC
    public bool paused { get; private set; }

    [Header("NPC Data")]
    [SerializeField] protected Animator anim; // reference to the animator component

    [Header("AI")]
    [SerializeField] private string restaurantTag = "PhysicalRestaurant";
    [SerializeField] protected private SphereCollider sc; // refrence to the sphere collider to turn on and off
    [SerializeField] protected private NavMeshAgent agent; // refrence to the nav mesh agent

    [Header("Targeting")]
    [SerializeField] private RestaurantList list;
    [SerializeField] protected private List<Sweets> sweets = new List<Sweets>();
    protected private Dictionary<Sweets, Transform> targetPairs = new Dictionary<Sweets, Transform>();
    private List<Sweets> validSweets = new List<Sweets>(); // keeps only the sweets that have locations
    protected private Transform target; // the target position
    protected private Sweets desiredSweet; // targeting sweet

    [Header("Wander Parameters")]
    [SerializeField] private float wanderRadius = 10f;

    [Header("Target UI")]
    [SerializeField] protected GameObject bubble;
    [SerializeField] protected Image bubbleIcon;

    private void Awake()
    {
        // Naman Khurana
        agent = GetComponent<NavMeshAgent>();
        // Joyce Mai
        agent.updateRotation = false; // this keeps the sprite facing the camera
        sc = GetComponent<SphereCollider>();
        bubble.SetActive(false);
    }

    protected void GetLocations() // creates a dictonary based on desired sweets and their locations
    {
        foreach (Sweets s in sweets)
        {
            Transform loc = list.GetLocation(s);
            if (loc) // only add valid locations to the dictionary
            {
                validSweets.Add(s); // keeps track of sweets with locations in a separate list
                targetPairs.Add(s, loc);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(restaurantTag) && other.transform == target)
        {
            print("I'm at my restaurant");
            Restaurant restaurant = other.GetComponent<Restaurant>();
            AtRestaurant(restaurant); // valid to pass a null restaurant location for loiterers
        }
        AdditionalTrigger(other);
    }

    private void OnEnable()
    {
        GameManager.OnWorldEnable += PlayAnim;
        GameManager.OnWorldDisable += PauseAnim;
    }

    private void OnDisable()
    {
        GameManager.OnWorldEnable -= PlayAnim;
        GameManager.OnWorldDisable -= PauseAnim;
    }

    private void PauseAnim()
    {
        anim.speed = 0;
        agent.isStopped = true;
        paused = true;
    }
    private void PlayAnim()
    {
        anim.speed = Time.timeScale;
        agent.isStopped = false;
        paused = false;
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public virtual Data GetData()
    {
        return null;
    }

    protected private void PickTarget() // chooses a random location from the diectionary
    {
        if (validSweets.Count <= 0) // don't pick a target if there are no targets
            return;

        target = null;
        while (target == null)
        {
            int index = Random.Range(0, validSweets.Count);
            desiredSweet = validSweets[index];
            target = targetPairs[desiredSweet];
        }
    }

    public virtual void Wander()
    {
        target = null;
        sc.enabled = false;

        Vector3 newPosition = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * wanderRadius;
        randomPos += transform.position;
        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, wanderRadius, 1))
        {
            newPosition = hit.position;
        }
        agent.SetDestination(newPosition);
    }

    public void SetCurrState(States state)
    {
        currState = state;
    }

    public virtual void Target()
    {
        PickTarget();

        if (target == null)
            return;

        sc.enabled = true; 
        bubble.SetActive(true);
        bubbleIcon.sprite = desiredSweet.soloIcon;
    }

    public virtual void AtRestaurant(Restaurant restaurant){}

    public virtual void AdditionalTrigger(Collider other){}

}
public class Data : ScriptableObject
{
    [SerializeField] float speed = 3f;

    [Header("Idle State")]
    [SerializeField] int idleTime = 3; // amount of time before moving again

    public float Speed()
    {
        return speed;
    }

    public int IdleTime()
    {
        return idleTime;
    }
}
