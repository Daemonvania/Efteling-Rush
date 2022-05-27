using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
public class ShowDistance : MonoBehaviour
{
    private GameObject player;

    private float initDist;

    private float distPercentage;
    public GameObject endOfDemoUI;
    private float distance;
    public Image progressBar;
    
    // Start is called before the first frame update
    void Start()
    {
        endOfDemoUI.SetActive(false);
        player = GameObject.Find("Player"); 
        initDist =  player.transform.position.x -  transform.position.x;
        print(initDist);
    }

    // Update is called once per frame
    void Update()
    {
        distance = player.transform.position.x - transform.position.x;
        distPercentage =  distance / initDist;
        progressBar.fillAmount = 1 - distPercentage;

        if (distance <= 0)
        {
            endOfDemoUI.SetActive(true);
        } 
    }
}
