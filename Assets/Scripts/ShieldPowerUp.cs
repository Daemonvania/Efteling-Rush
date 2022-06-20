using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPowerUp : MonoBehaviour
{
    private Player _player;
    [SerializeField] float powerUpDuration = 1;
    public ParticleSystem powerUpParticle;
    public GameObject shield;
    
    
    public AudioSource powerUpSound;
    private bool isActive;
    public Image _durationImage;
    private float timeElapsed;

    private void Start()
    {
        shield.SetActive(false);
        _durationImage.gameObject.SetActive(false);
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {   
            StopAllCoroutines();
            shield.SetActive(true);
            powerUpSound.Play();
            Destroy(other.gameObject);
            _durationImage.gameObject.SetActive(true);
            isActive = true;
            timeElapsed = 0;
            _durationImage.fillAmount = 0;
            _player.canTakeDamage = false;
            powerUpParticle.Play();
            StartCoroutine(DisableDamage());    
        }
    }

    private void Update()
    {
        if (isActive)
        {
           _durationImage.fillAmount = Mathf.Lerp(1, 0, timeElapsed/powerUpDuration);
            timeElapsed += Time.deltaTime;
        }
    }

    IEnumerator DisableDamage()
    {
        yield return new WaitForSeconds(powerUpDuration);
        _player.canTakeDamage = true;
        shield.SetActive(false);
        powerUpParticle.Stop();
        _durationImage.gameObject.SetActive(false);
    }
}
