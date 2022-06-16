using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndVogel : MonoBehaviour
{
    public float initDist;
    private float distance;
    private Transform player;
    public Image vogelDurationImage;

    private bool isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        vogelDurationImage.gameObject.SetActive(false);
    }

    public void EnableDuration(bool enable)
    {
        vogelDurationImage.gameObject.SetActive(enable);
        isEnabled = enable;
        vogelDurationImage.fillAmount = 0;
    }
        
    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            distance = player.transform.position.x - transform.position.x;
            vogelDurationImage.fillAmount = distance / initDist;
            print(distance);
        }
    }
}
