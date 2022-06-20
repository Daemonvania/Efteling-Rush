using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PlayerData
{
    public int currentLevel;
    public int ticketCount;
    public float couponProgress;
    public string[] hatsUnlocked;
    public string activeHat;
    public float audioLevel;

    public PlayerData (DontDestroyOnLoad dontDestroyOnLoad)
    {
        currentLevel = dontDestroyOnLoad.currentLevel;
        ticketCount = dontDestroyOnLoad.tickets;
        couponProgress = dontDestroyOnLoad.couponProgress;
        hatsUnlocked = dontDestroyOnLoad.unlockedHats.ToArray();
        activeHat = dontDestroyOnLoad.activeHat;
        audioLevel = dontDestroyOnLoad.audioLevel;
    }
}
