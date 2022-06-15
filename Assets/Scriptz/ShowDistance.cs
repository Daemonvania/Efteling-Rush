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

    private float initCouponFill;
    public Image couponBar;

    private bool isShowingCoupon = false;
    private bool endedlevel = false;


    private float LerpNumber = 0;
    // Start is called before the first frame update
    void Start()
    {   
        _dontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
        initCouponFill = _dontDestroyOnLoad.couponProgress;
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
            if (!endedlevel)
            {
                endedlevel = true;
                Time.timeScale = 0;
                endOfLevelUI.SetActive(true);
                _dontDestroyOnLoad.couponProgress += 0.1f;
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
            if (LerpNumber>= 1)
            {
                isShowingCoupon= false;
            }
            print(LerpNumber);
        } 
    }
}
