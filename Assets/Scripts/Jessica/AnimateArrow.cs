using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jessica Lam
public class AnimateArrow : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Pressed");
        }
    }

    void OnMouseOver(){
        anim.SetBool("Hovering", true);
    }

    void OnMouseExit()
    {
        anim.SetBool("Hovering", false);
    }
}
