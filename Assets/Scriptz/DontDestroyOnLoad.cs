using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{ 
    [HideInInspector] public int tickets = 0;
    [HideInInspector] public string activeHat;     
    private void Start()
    {
        activeHat = "noHat";
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tickets += 100;
        }
    }
}
