using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    public GameObject[] Hats;

    private DontDestroyOnLoad _dontDestroyOnLoad;

    private bool activatedHat = false; 
    private void Start()
    {
        _dontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
        ChangeHat(_dontDestroyOnLoad.activeHat);
    }

    public void ChangeHat(string hatName)
    {
        foreach (GameObject hat in Hats)
        {
            Hat currentHat;
            currentHat = hat.GetComponent<Hat>();
            if (hat.name == hatName && _dontDestroyOnLoad.tickets >= currentHat.hatPrice || hat.name == hatName && currentHat.isUnlocked)
            {
                hat.SetActive(true);
                activatedHat = true;
                if (!currentHat.isUnlocked)
                {
                    currentHat.isUnlocked = true;
                    _dontDestroyOnLoad.tickets -= currentHat.hatPrice;
                }
                
                _dontDestroyOnLoad.activeHat = currentHat.gameObject.name;
                print(_dontDestroyOnLoad.activeHat);
            }
            else
            {
                hat.SetActive(false);
            }

            print(activatedHat);
        }

        if (activatedHat == false && _dontDestroyOnLoad.activeHat != "noHat")
        {
            print("shitruns");
            ChangeHat(_dontDestroyOnLoad.activeHat);
        }
        else
        {
            activatedHat = false;
        }
    }
}
