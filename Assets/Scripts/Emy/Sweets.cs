using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Emily Chavez
[CreateAssetMenu]
public class Sweets : ScriptableObject
{
    [Header("Visuals")]
    public string sweetName;
    public Sprite icon;
    public Sprite UIIcon;
    public Sprite soloIcon;

    [Header("Number Values")]
    public float growthTime;
    public int hungerFillAmount;
    public int price;

    [Header("Kitchen Sprites")]
    public Sprite startSprite;
    public Sprite P1GoodSprite;
    public Sprite P1BadSprite;
    public Sprite P1GoodP2BadSprite;
    public Sprite P1BadP2BadSprite;
    public Sprite P1GoodP2GoodSprite;
    public Sprite P1BadP2GoodSprite;

    [Header("Music")]
    public AudioClip BGMusic;
}
