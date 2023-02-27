using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
[CreateAssetMenu]
public class DirtUISettings : ScriptableObject
{
    [SerializeField] private Color growCol; // a color for the bar when growing
    [SerializeField] private Color waitCol; // a color for the bar when on cooldown

    public Color GrowCol()
    {
        return growCol;
    }
    public Color WaitCol()
    {
        return waitCol;
    }
}
