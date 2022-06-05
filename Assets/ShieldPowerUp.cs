using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    private Player _player;
    [SerializeField] float powerUpDuration = 1;
    public ParticleSystem powerUpParticle;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            Destroy(other.gameObject);
            _player.canTakeDamage = false;
            powerUpParticle.Play();
            StartCoroutine(DisableDamage());    
        }
    }


    IEnumerator DisableDamage()
    {
        yield return new WaitForSeconds(powerUpDuration);
        _player.canTakeDamage = true;
        powerUpParticle.Stop();
    }
}
