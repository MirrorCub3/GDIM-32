using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai & Naman Khurana
public class Eater : NPC
{
    [Header("AI Variables")]
    [SerializeField] private EaterData myData;

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

