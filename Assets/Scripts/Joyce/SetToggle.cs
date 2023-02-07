using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class SetToggle : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.SetOuterWorldToggle(this.gameObject);
    }
}
