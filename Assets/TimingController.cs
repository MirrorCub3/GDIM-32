using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingController : MonoBehaviour
{   
public float Timer = 2;
    public GameObject dessertPrefab;
    GameObject dessertClone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            dessertClone = Instantiate(dessertPrefab, new Vector3(Random.Range(-9, 9), 5f, 0f), transform.rotation) as GameObject;
            Timer = 2f;
        }
    }
}
