using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai and Naman Khurana
public class Visitor : NPC
{
    [Header("AI Variables")]
    [SerializeField] private VisitorData myData;

    void Update()
    {
        // Naman Khurana
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public override void AtRestaurant() // defines what this AI does when at the restuarant
    {
        sc.enabled = false;
        StartCoroutine(Visiting());
    }

    public IEnumerator Visiting() // defines the waiting state of the AI
    {
        int waitTime = Random.Range(myData.VisitTimeMin(), myData.VisitTimeMax()); // picks a loiter time based on the given interval 
        Debug.Log("visiting for " + waitTime);

        yield return new WaitForSeconds(waitTime);
        PickTarget();
        sc.enabled = true;
    }
}
