using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;    
    }

    
    public void UnFreeze()
    {
        Time.timeScale = 1f;
    }
}
