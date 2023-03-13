using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joyce Mai
public class Strategy : MonoBehaviour
{
    [SerializeField] protected PlayerAI me; // reference to the ai
    [SerializeField] protected Animator anim; // reference to animator
    [SerializeField] protected Transform player; // reference to the player 1 target
    [SerializeField] protected NavMeshAgent agent; // reference to the NavMesh agent
    [SerializeField] protected float speed = 3f; // speed during this state
    private void Awake()
    {
        agent.updateRotation = false;
    }

    protected void Flip() // flips based on direction
    {
        if (agent.destination.x <= transform.position.x)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

    }
}
