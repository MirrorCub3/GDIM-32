using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Joyce Mai
public class Strategy : MonoBehaviour
{
    [SerializeField] protected PlayerAI me;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform player;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float speed = 3f;
    private void Awake()
    {
        agent.updateRotation = false;
    }

    protected void Flip()
    {
        if (agent.destination.x <= transform.position.x)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

    }
}
