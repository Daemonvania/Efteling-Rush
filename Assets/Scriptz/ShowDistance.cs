using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ShowDistance : MonoBehaviour
{
    private GameObject player;

    private float initDist;

    private float distPercentage;
    public GameObject endOfLevelUI;
    private float distance;
    public Image progressBar;
    
    // Start is called before the first frame update
    void Start()
    {
        endOfLevelUI.SetActive(false);
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
            Time.timeScale = 0;
            endOfLevelUI.SetActive(true);
        } 
    }
}
