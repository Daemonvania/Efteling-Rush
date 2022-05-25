using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private int colCollected = 0;

    public ParticleSystem particleSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            colCollected++;
            particleSystem.Play();
        }
    }
}
