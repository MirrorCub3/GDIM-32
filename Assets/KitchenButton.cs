using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenButton : MonoBehaviour
{
    Animator anim;
    string whichButton;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        whichButton = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (whichButton == "E Button"){
            if (Input.GetKeyDown(KeyCode.E)){
                anim.SetTrigger("Pressed");
            }
        }
        if (whichButton == "P Button"){
            if (Input.GetKeyDown(KeyCode.P)){
                anim.SetTrigger("Pressed");
            }
        }
    }
}
