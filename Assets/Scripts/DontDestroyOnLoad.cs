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

    [HideInInspector] public int currentLevel;
    [HideInInspector] public float couponProgress = 0;
    
    private void Awake()
    {
        activeHat = "noHat";
        currentLevel = 1;
        
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            activeHat = data.activeHat;
            currentLevel = data.currentLevel;
            tickets = data.ticketCount;
            couponProgress = data.couponProgress;
            unlockedHats = new List<string>(data.hatsUnlocked);
        }
        
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
