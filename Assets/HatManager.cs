using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    public GameObject[] Hats;

    private DontDestroyOnLoad _dontDestroyOnLoad;


    private void Start()
    {
        _dontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
    }

    public void ChangeHat(string hatName)
    {
        foreach (GameObject hat in Hats)
        {
            Hat currentHat;
            currentHat = hat.GetComponent<Hat>();
            if (hat.name == hatName && _dontDestroyOnLoad.tickets > currentHat.hatPrice || hat.name == hatName && currentHat.isUnlocked)
            {
                hat.SetActive(true);
                currentHat.isUnlocked = true;
            }
            else
            {
                hat.SetActive(false);
            }
        }    
    }
}
