using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestroyOnLoad : MonoBehaviour
{ 
    [HideInInspector] public int tickets = 0;
    [HideInInspector] public string activeHat;
    /*[HideInInspector]*/ public List<String> unlockedHats = new List<String>();
    
    private void Start()
    {
        activeHat = "noHat";
        DontDestroyOnLoad(this);
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tickets += 1000;
        }
    }

    public void HidePrices()
    {
        foreach (string hatname in unlockedHats)
        {
            print(hatname);
            if (GameObject.Find(hatname + "Price") != null)
            {
                GameObject.Find(hatname + "Price").SetActive(false);
            }
        }
    }
}
