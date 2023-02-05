using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai and Naman Khurana
public class Visitor : NPC
{
    [Header("AI Variables")]
    [SerializeField] private VisitorData myData;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
