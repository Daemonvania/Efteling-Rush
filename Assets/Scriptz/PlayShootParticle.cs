using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShootParticle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HighObstacle") || other.CompareTag("LowObstacle") || other.CompareTag("MiddleObstacle"))
        {
             particleSystem.Play();
        }

    }
}
