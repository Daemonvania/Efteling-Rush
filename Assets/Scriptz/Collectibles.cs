using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private int colCollected = 0;

    public ParticleSystem particleSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;  
            colCollected++;
            particleSystem.Play();
        }
    }
}
