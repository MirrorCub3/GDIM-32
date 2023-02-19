using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator anim; // player's animator

    void Start()
    {
        // Get this gameobjects Animator component
        anim = gameObject.GetComponent<Animator>();
    }

    public void Fail()
    {
        anim.SetTrigger("Fail");
    }
}
