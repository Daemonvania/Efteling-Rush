using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TicketPowerup : MonoBehaviour
{
    private Collectibles collectibles;
    [SerializeField] float powerUpDuration = 1;
    public ParticleSystem powerUpParticle;

    private bool isActive;
    public Image _durationImage;
    private float timeElapsed;

    private Collectibles _dontDestroyOnLoad;
    private void Start()
    {
        _durationImage.gameObject.SetActive(false);
        collectibles = GetComponent<Collectibles>();
    }

    private void Update()
    {
        if (isActive)
        {
            _durationImage.fillAmount = Mathf.Lerp(1, 0, timeElapsed/powerUpDuration);
            timeElapsed += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TicketPowerUp"))
        {
            Destroy(other.gameObject);
            _durationImage.gameObject.SetActive(true);
            isActive = true;
            timeElapsed = 0;
            _durationImage.fillAmount = 0;
            collectibles.doubleCollectibles = true;
            StartCoroutine(DisablePowerup());    
        }
    }

    IEnumerator DisablePowerup()
    {
        yield return new WaitForSeconds(powerUpDuration);
        collectibles.doubleCollectibles = false;
        _durationImage.gameObject.SetActive(false);
    }
}
