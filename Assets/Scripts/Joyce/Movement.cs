using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private int playerNumber = 1; // this will affect which controls the player uses
    [SerializeField] private CharacterController controller;  // reference to the player controller for 3D movement
    [SerializeField] private float speed = 3f; // speed of the player's movement

    // bookkeeping variables for the movement axes
    private string xAxisName;
    private string yAxisName;

    [Header("Visuals")]
    [SerializeField] private Animator anim;  // reference to the player's animator

    private void Start()
    {
        // setting the axes names
        xAxisName = "Horizontal" + playerNumber;
        yAxisName = "Vertical" + playerNumber;
    }

    void Update()
    {
        if (Time.deltaTime.Equals(0)) // makes sure the player doesnt move when paused
            return;

        float horizontal = Input.GetAxisRaw(xAxisName);
        float vertical = Input.GetAxisRaw(yAxisName);
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized; // creates a new vector for movement direction

        anim.SetFloat("Speed", Mathf.Abs(direction.magnitude)); // setting the speed variable in the animator to change animation states
        if (Mathf.Abs(horizontal) > 0) // flips the object once the player moves in a direction
            transform.localScale = new Vector3(-horizontal, transform.localScale.y, transform.localScale.z); // sprite is left facing so the scale must be set to -horizontal

        controller.Move(direction * speed * Time.deltaTime); // applys movement
        
    }
}
