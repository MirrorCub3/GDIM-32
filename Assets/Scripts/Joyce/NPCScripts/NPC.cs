using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joyce Mai and Naman Khurana
public class NPC : MonoBehaviour
{
    [Header("NPC Data")]
    [SerializeField] private Animator anim; // reference to the animator component

    [Header("AI")]
    [SerializeField] private string restaurantTag = "PhysicalRestaurant";
    [SerializeField] protected private SphereCollider sc; // refrence to the sphere collider to turn on and off
    [SerializeField] protected private NavMeshAgent agent; // refrence to the nav mesh agent
    [SerializeField] protected private List<Transform> targets = new List<Transform>(); // adjust this to something more dynamic later for spawning purposes
    protected private Transform target; // the target position

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
    protected private void PickTarget() // chooses a random location from the list
    {
        if (targets.Count <= 0) // don't pick a target if there are no targets
            return;

        int index = Random.Range(0, targets.Count);
        target = targets[index];
    }

    public virtual void AtRestaurant(Restaurant restaurant){}

    public virtual void AdditionalTrigger(Collider other){}

}
