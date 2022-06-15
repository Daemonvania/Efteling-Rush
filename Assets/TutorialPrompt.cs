using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialPrompt : MonoBehaviour
{
    public GameObject tutorialImage;
    [FormerlySerializedAs("swipeMovement")] public string swipeDirection;
    private Player _player;
    private bool isShowingTutorial;


    private bool ducked;
    private bool moved;
    private bool jumped;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        tutorialImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0.1f;
            tutorialImage.SetActive(true);
            isShowingTutorial = true;
        }
    }


    private void Update()
    {
 
        if (isShowingTutorial)
        {
            Swipe();
            if (moved == true && swipeDirection == "side")
            {
                Time.timeScale = 1;
                tutorialImage.SetActive(false);
            }
            
            if (jumped == true && swipeDirection == "up")
            {
                Time.timeScale = 1;
                tutorialImage.SetActive(false);
              //  _player.Jump();
            }
            if (ducked == true && swipeDirection == "down")
            {
                Time.timeScale = 1;
                tutorialImage.SetActive(false);
                //  _player.Jump();
            }
        }
    }
    
    
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 40f;

    // Update is called once per frame
    void Swipe()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumped = true;
            StartCoroutine(resetStuff());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            moved= true;
            StartCoroutine(resetStuff());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moved= true;
            StartCoroutine(resetStuff());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ducked = true;
            StartCoroutine(resetStuff());
        }

        
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began) {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
            }

            //Detects Swipe while finger is still moving on screen
            if (touch.phase == TouchPhase.Moved) {
                if (!detectSwipeAfterRelease) {
                    fingerDownPos = touch.position;
                    DetectSwipe ();
                }
            }

            //Detects swipe after finger is released from screen
            if (touch.phase == TouchPhase.Ended) {
                fingerDownPos = touch.position;
                DetectSwipe ();
            }
        }
    }

    void DetectSwipe ()
    {

       
        
        
        if (VerticalMoveValue () > SWIPE_THRESHOLD && VerticalMoveValue () > HorizontalMoveValue ()) {
            Debug.Log ("Vertical Swipe Detected!");
            if (fingerDownPos.y - fingerUpPos.y > 0) {
                OnSwipeUp ();
            } else if (fingerDownPos.y - fingerUpPos.y < 0) {
                OnSwipeDown ();
            }
            fingerUpPos = fingerDownPos;

        } else if (HorizontalMoveValue () > SWIPE_THRESHOLD && HorizontalMoveValue () > VerticalMoveValue ()) {
            Debug.Log ("Horizontal Swipe Detected!");
            if (fingerDownPos.x - fingerUpPos.x > 0) {
                OnSwipeRight ();
            } else if (fingerDownPos.x - fingerUpPos.x < 0) {
                OnSwipeLeft ();
            }
            fingerUpPos = fingerDownPos;

        } else {
            Debug.Log ("No Swipe Detected!");
        }
    }

    float VerticalMoveValue ()
    {
        return Mathf.Abs (fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue ()
    {
        return Mathf.Abs (fingerDownPos.x - fingerUpPos.x);
    }

    void OnSwipeUp ()
    {
        jumped = true;
        StartCoroutine(resetStuff());
    }

    void OnSwipeDown ()
    {
        ducked = true;
    }

    void OnSwipeLeft ()
    {
        moved = true;
        StartCoroutine(resetStuff());
    }

    void OnSwipeRight ()
    {
        moved = true;
        StartCoroutine(resetStuff());
    }


    IEnumerator resetStuff()
    {
        yield return new WaitForSeconds(0.1f);
        moved = false;
        jumped = false;
        ducked = false;
        isShowingTutorial = false;
    }
}

