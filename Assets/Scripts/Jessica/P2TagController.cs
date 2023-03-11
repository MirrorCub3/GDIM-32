using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2TagController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance)
        {
            if (GameManager.instance.playmode == GameManager.PlayMode.SINGLE)
            {
                gameObject.SetActive(false);
            }
            else if (GameManager.instance.playmode == GameManager.PlayMode.MULTI)
            {
                gameObject.SetActive(true); // maybe can replace with a tag that says AI
            }
        }
    }

}
