using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class HatManager : MonoBehaviour
{
    public GameObject[] Hats;

    [Space] public GameObject femaleHair;
    
    private DontDestroyOnLoad _dontDestroyOnLoad;

    public GameObject femaleHairImage;
    
    
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
            if (hat.name == hatName && _dontDestroyOnLoad.tickets >= currentHat.hatPrice || hat.name == hatName && _dontDestroyOnLoad.unlockedHats.Contains(hat.name))
            {
                hat.SetActive(true);
                activatedHat = true;
                if (!_dontDestroyOnLoad.unlockedHats.Contains(hat.name))
                {
                    _dontDestroyOnLoad.unlockedHats.Add(hat.name);
                    _dontDestroyOnLoad.tickets -= currentHat.hatPrice;
                }
                
                _dontDestroyOnLoad.activeHat = currentHat.gameObject.name;
            }
            else
            {
                hat.SetActive(false);
            }
            
        }

        if (activatedHat == false && _dontDestroyOnLoad.activeHat != "noHat")
        {
            //print("shitruns");
            ChangeHat(_dontDestroyOnLoad.activeHat);
        }
        else
        {
            activatedHat = false;
        }
        
        
        
        _dontDestroyOnLoad.HidePrices();
    }

    public void EnableFemaleHair()
    {
        if (!femaleHair.activeSelf)
        {
            femaleHair.SetActive(true);
            femaleHairImage.SetActive(true);
        }
        else
        {
            femaleHair.SetActive(false);
            femaleHairImage.SetActive(false);
        }
    }
}
