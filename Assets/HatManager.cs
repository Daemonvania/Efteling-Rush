using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    public GameObject[] Hats;
    
    void ChangeHat(string hatName)
    {
        foreach (GameObject hat in Hats)
        {
            if (hat.name == hatName)
            {
                hat.SetActive(true);
            }
            else
            {
                hat.SetActive(false);
            }
        }    
    }
}
