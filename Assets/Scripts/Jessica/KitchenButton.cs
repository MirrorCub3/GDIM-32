using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jessica Lam
public class KitchenButton : MonoBehaviour
{
    private Animator anim;
    private string whichButton;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        whichButton = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (whichButton == "E Button"){// if this script is on E
            if (Input.GetKeyDown(KeyCode.E)){
                anim.SetTrigger("Pressed");
                FindObjectOfType<AudioManager>().Play("Button Press");
            }
        }
        if (whichButton == "P Button"){// if this script is on P
            if (Input.GetKeyDown(KeyCode.P)){
                anim.SetTrigger("Pressed");
                FindObjectOfType<AudioManager>().Play("Button Press");
            }
        }
    }
}
