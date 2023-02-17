using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessertController : MonoBehaviour
{   
    SpriteRenderer spriteRenderer;
    public Sweets sweetObject;

    string keypressCode; // changes depending on P1 or P2

    bool withinRange; // determines if dessert is withinRange

    // Start is called before the first frame update
    void Start()
    {
        // Get this gameobjects SpriteRenderer component
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        keypressCode = "e";
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput(keypressCode);
    }

    // When this dessert reaches the perfect placement window 
    void OnTriggerEnter2D (Collider2D other)
    {   
        // Dessert reaches Player 1's placement
        if (other.name == "Perfect Placement P1")
        {
            withinRange = true;
            keypressCode = "e";
        }
        // Dessert reaches Player 1's placement
        else if (other.name == "Perfect Placement P2")
        {   
            withinRange = true;
            keypressCode = "p";
        }
        // Dessert reaches Player 1's placement too early
        else if (other.name == "Too Early Placement")
        {
            keypressCode = "e";
        }
        // Dessert reaches Player 2's placement too early
        else if (other.name == "Too Early Placement P2")
        {
            keypressCode = "p";
        }

    }

    // When this dessert exits the perfect placement window
    void OnTriggerExit2D (Collider2D other)
    {
        withinRange = false;
        // if dessert exits the placement and the dessert sprite hasn't changed, then it must be late & bad
        if (keypressCode == "e"){
            if (spriteRenderer.sprite == sweetObject.startSprite){
                spriteRenderer.sprite = sweetObject.P1BadSprite;
            }
        }
        else if (keypressCode == "p"){
            if (spriteRenderer.sprite == sweetObject.P1GoodSprite){
                spriteRenderer.sprite = sweetObject.P1GoodP2BadSprite;
            }
            else if (spriteRenderer.sprite == sweetObject.P1BadSprite){
                spriteRenderer.sprite = sweetObject.P1BadP2BadSprite;
            }
        }
    }

    // Checks if input buttons are pressed
    void CheckInput(string keypressCode)
    {
        if (Input.GetKeyDown(keypressCode) && withinRange){
            if (keypressCode == "e" && spriteRenderer.sprite != sweetObject.P1BadSprite){
                spriteRenderer.sprite = sweetObject.P1GoodSprite;
            }
            else if (keypressCode == "p"){
                if (spriteRenderer.sprite == sweetObject.P1GoodSprite){
                    spriteRenderer.sprite = sweetObject.P1GoodP2GoodSprite;
                }
                else if (spriteRenderer.sprite == sweetObject.P1BadSprite){
                    spriteRenderer.sprite = sweetObject.P1BadP2GoodSprite;
                }
            }
        }
        else if (Input.GetKeyDown(keypressCode) && !withinRange){
            if (keypressCode == "e"){
                spriteRenderer.sprite = sweetObject.P1BadSprite;
            }
            else if (keypressCode == "p"){
                if (spriteRenderer.sprite == sweetObject.P1GoodSprite){
                    spriteRenderer.sprite = sweetObject.P1GoodP2BadSprite;
                }
                else if (spriteRenderer.sprite == sweetObject.P1BadSprite){
                    spriteRenderer.sprite = sweetObject.P1BadP2BadSprite;
                }
            }
        }
    }
}
