using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 3f;

    //[Header("Visuals")]
    // variables for potential animations

    void Update()
    {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized; // creates a new vector for 

            // stuff to change animation states

            controller.Move(direction * speed * Time.deltaTime);
        
    }
}
