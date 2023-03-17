using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jessica Lam
public class CreditsTab : MonoBehaviour
{
    [SerializeField] private GameObject content;
    
    void Start()
    {
        content.SetActive(false);
    }

    public void CloseCredits()
    {
        content.SetActive(false);
    }

    public void OpenCredits()
    {
        content.SetActive(true);
    }
}
