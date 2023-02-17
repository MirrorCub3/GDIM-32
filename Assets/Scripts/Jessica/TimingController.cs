using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingController : MonoBehaviour
{   
    float Timer = 2f;
    public GameObject dessertPrefab;
    GameObject dessertClone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Creates a new dessert every 2 seconds
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            dessertClone = Instantiate(dessertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Debug.Log("Created new dessert");
            Timer = 2f;
        }
    }
}
