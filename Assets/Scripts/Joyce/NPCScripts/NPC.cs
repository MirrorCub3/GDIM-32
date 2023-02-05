using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joyce Mai
public class NPC : MonoBehaviour
{
    [Header("NPC Data")]
    [SerializeField] private Animator anim; // reference to the animator component
    [SerializeField] private float speed; // speed for the movement of the npc

    [Header("AI")]
    [SerializeField] private NavMeshAgent agent; // refrence to the nav mesh agent
    protected private Transform target; // the target position

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Restaurant"))
        {
            AtRestaurant();
        }
    }

    public virtual void AtRestaurant() // have child classes override this with their own behavior
    {
        Debug.Log("The base NPC Class does nothing on arrival");
    }

}
