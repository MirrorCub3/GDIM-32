using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] protected private List<Transform> targets = new List<Transform>(); // adjust this to something more dynamic later for spawning purposes
    protected private Transform target; // the target position

    [Header("Wander Parameters")]
    [SerializeField] private float wanderRadius = 10f;

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

    protected private void PickTarget() // chooses a random location from the list
    {
        if (targets.Count <= 0) // don't pick a target if there are no targets
            return;

        int index = Random.Range(0, targets.Count);
        target = targets[index];
        
        // used to debug// very important this is game breaking
        if(target == null)
            Debug.Log("NULL RESTAURANT IN LIST: THIS IS BAD");
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
        sc.enabled = true;
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
