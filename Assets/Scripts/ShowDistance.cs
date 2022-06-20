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


    private DontDestroyOnLoad _dontDestroyOnLoad;
    private float distPercentage;
    public GameObject endOfLevelUI;
    private float distance;
    public Image progressBar;

    public GameObject showCouponButton;
    private float initCouponFill;
    public Image couponBar;

    private bool isShowingCoupon = false;
    private bool endedlevel = false;


    private float LerpNumber = 0;
    // Start is called before the first frame update
    void Start()
    {   
        _dontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
        showCouponButton.SetActive(false);
        initCouponFill = _dontDestroyOnLoad.couponProgress;
        endOfLevelUI.SetActive(false);
        player = GameObject.Find("Player"); 
        initDist =  player.transform.position.x -  transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        distance = player.transform.position.x - transform.position.x;
        distPercentage =  distance / initDist;
        progressBar.fillAmount = 1 - distPercentage;

        if (distance <= 0)
        {
            if (!endedlevel)
            {
                endedlevel = true;
                SaveSystem.SavePlayer(_dontDestroyOnLoad);
                Time.timeScale = 0;
                endOfLevelUI.SetActive(true);
                _dontDestroyOnLoad.couponProgress += 0.1f;
                _dontDestroyOnLoad.currentLevel++;
                isShowingCoupon = true;
            }
        }

        ShowCouponBar();
    }

    void ShowCouponBar()
    {
        if (isShowingCoupon)
        {
         
            couponBar.fillAmount = Mathf.Lerp(initCouponFill, _dontDestroyOnLoad.couponProgress, LerpNumber);
            LerpNumber += 0.7f  * Time.unscaledDeltaTime;
            Physics.SyncTransforms();
            //     characterController.Move(new Vector3(0, 0, lerpValue));
            if (couponBar.fillAmount > 0.99f)
            {
                showCouponButton.SetActive(true);
            }
            
            if (LerpNumber>= 1)
            {
                isShowingCoupon= false;
            }

      
        } 
    }
}
