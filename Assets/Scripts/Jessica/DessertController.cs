using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jessica Lam
// Leaf class
public class DessertController : MonoBehaviour, Quality
{
    //[SerializeField] private GameManager gameManager; // knows if this is single player or multiplayer
    public bool singleplayer;

    SpriteRenderer spriteRenderer; // get current spriterenderer to change sprite during runtime
    [SerializeField] private Sweets sweetObject; // get the correct sweet object

    string keypressCode; // changes depending on P1 or P2

    bool withinRange; // determines if dessert is withinRange

    public float quality { get; private set; } // quality rating of this dessert

    void Start()
    {
        // Get this gameobjects SpriteRenderer component
        singleplayer = true;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        keypressCode = "e";
        quality = 0f;
    }

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
                FindObjectOfType<AudioManager>().Play("Dessert Whiff");
            }
        }
        else if (keypressCode == "p" && singleplayer == false)
        {
            if (spriteRenderer.sprite == sweetObject.P1GoodSprite){
                spriteRenderer.sprite = sweetObject.P1GoodP2BadSprite;
                quality = 2f;
                FindObjectOfType<AudioManager>().Play("Dessert Whiff");
            }
            else if (spriteRenderer.sprite == sweetObject.P1BadSprite){
                spriteRenderer.sprite = sweetObject.P1BadP2BadSprite;
                quality = 0f;
                FindObjectOfType<AudioManager>().Play("Dessert Whiff");
            }
        }
    }

    // Checks if input buttons are pressed
    void CheckInput(string keypressCode)
    {
        if (keypressCode == "p" && singleplayer == true && withinRange)
        {
            if (spriteRenderer.sprite == sweetObject.P1GoodSprite)
            {
                spriteRenderer.sprite = sweetObject.P1GoodP2GoodSprite;
                quality = 3f;
                FindObjectOfType<AudioManager>().Play("Dessert Success");
            }
            else if (spriteRenderer.sprite == sweetObject.P1BadSprite)
            {
                spriteRenderer.sprite = sweetObject.P1BadP2GoodSprite;
                quality = 2f;
                FindObjectOfType<AudioManager>().Play("Dessert Success");
            }
        }
        // if pressed on time
        if (Input.GetKeyDown(keypressCode) && withinRange){
            if (keypressCode == "e" && spriteRenderer.sprite != sweetObject.P1BadSprite){
                spriteRenderer.sprite = sweetObject.P1GoodSprite;
                FindObjectOfType<AudioManager>().Play("Dessert Success");
            }
            else if (keypressCode == "p"){
                if (spriteRenderer.sprite == sweetObject.P1GoodSprite){
                    spriteRenderer.sprite = sweetObject.P1GoodP2GoodSprite;
                    quality = 3f;
                    FindObjectOfType<AudioManager>().Play("Dessert Success");
                }
                else if (spriteRenderer.sprite == sweetObject.P1BadSprite){
                    spriteRenderer.sprite = sweetObject.P1BadP2GoodSprite;
                    quality = 2f;
                    FindObjectOfType<AudioManager>().Play("Dessert Success");
                }
            }
        }
        // if pressed too early
        else if (Input.GetKeyDown(keypressCode) && !withinRange){
            if (keypressCode == "e"){
                spriteRenderer.sprite = sweetObject.P1BadSprite;
                FindObjectOfType<AudioManager>().Play("Dessert Whiff");
            }
            else if (keypressCode == "p" && singleplayer == false){
                if (spriteRenderer.sprite == sweetObject.P1GoodSprite){
                    spriteRenderer.sprite = sweetObject.P1GoodP2BadSprite;
                    quality = 2f;
                    FindObjectOfType<AudioManager>().Play("Dessert Whiff");
                }
                else if (spriteRenderer.sprite == sweetObject.P1BadSprite){
                    spriteRenderer.sprite = sweetObject.P1BadP2BadSprite;
                    quality = 0f;
                    FindObjectOfType<AudioManager>().Play("Dessert Whiff");
                }
            }
        }
    }

    // returns the quality of this dessert
    public float GetQuality(){
        return quality;
    }
}