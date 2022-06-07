using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private DontDestroyOnLoad _dontDestroyOnLoad;
    
    public ParticleSystem particleSystem;

    private int _tickets = 0;
    public TMP_Text ticketCounter;
    private void Start()
    {
        _dontDestroyOnLoad = GameObject.FindObjectOfType<DontDestroyOnLoad>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            particleSystem.Play();
            Destroy(other.gameObject);
            _dontDestroyOnLoad.tickets++;
            _tickets++;
            ticketCounter.text = _tickets.ToString();
   
        }
    }
}
