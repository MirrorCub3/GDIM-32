using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Joyce Mai and Naman Khurana
public class Visitor : NPC
{
    [Header("AI Variables")]
    [SerializeField] private VisitorData myData;
    [SerializeField] Transform homeBase;

    [Header("UI")]
    [SerializeField] private GameObject timerObject;
    [SerializeField] private Slider timerSlider;
    private void Awake()
    {
        // Naman Khurana
        agent = GetComponent<NavMeshAgent>();

        // Joyce Mai
        sc = GetComponent<SphereCollider>();
        agent.updateRotation = false; // this keeps the sprite facing the camera
        agent.speed = myData.Speed();

        timerObject.SetActive(false);

        PickTarget();
    }

    void Update()
    {
        // Naman Khurana
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public override void AtRestaurant(Restaurant restaurant) // defines what this AI does when at the restuarant
    {
        sc.enabled = false;
        StopAllCoroutines();
        StartCoroutine(Visiting());
    }

    public override void AdditionalTrigger(Collider other)
    {
        // visitors have an additional target of home base to idle at
        if (other.transform == homeBase && homeBase == target)
        {
            Debug.Log("back at home base");
            StartCoroutine(Visiting(myData.IdleTime()));
        }
    }

    protected IEnumerator Visiting(int specificTime = -1) // defines the waiting state of the AI
    {
        timerObject.SetActive(true);
        target = null;

        int waitTime = specificTime;

        if (waitTime < 0) // choose a random time if none is given
                waitTime = Random.Range(myData.VisitTimeMin(), myData.VisitTimeMax()); // picks a loiter time based on the given interval 

        timerSlider.maxValue = waitTime;
        timerSlider.value = waitTime;

        while (timerSlider.value > 0) // updates the slider to display timer
        {
            timerSlider.value -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // after the wait time, pick a new target location
        PickTarget();
        sc.enabled = true;
        timerObject.SetActive(false);
    }
}
