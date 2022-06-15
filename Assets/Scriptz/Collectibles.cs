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

    [HideInInspector]  public bool doubleCollectibles = false;

    public AudioSource collectTicketSound;

    
    
    
    private float SoundTimer = 0;
    
    private int _tickets = 0;
    public TMP_Text ticketCounter;
    private void Start()
    {
        _dontDestroyOnLoad = GameObject.FindObjectOfType<DontDestroyOnLoad>();
    }

    private void Update()
    {
        SoundTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
           
            particleSystem.Play();
            Destroy(other.gameObject);

            if (SoundTimer <= 0.3f)
            {
                collectTicketSound.pitch += 0.1f;
            }
            else
            {
                collectTicketSound.pitch = 1;
            }
            collectTicketSound.Play();
            SoundTimer = 0;
            if (doubleCollectibles)
            {
                _tickets += 3;
                _dontDestroyOnLoad.tickets+= 3;
                print("MONEY");
            }
            else
            {
                _tickets++;
                _dontDestroyOnLoad.tickets++;
            }

            ticketCounter.text = _tickets.ToString();
   
        }
    }
}
