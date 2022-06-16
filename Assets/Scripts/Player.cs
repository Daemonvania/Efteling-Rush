using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    //WARNING TO DYLAN: THERE IS SOME TERRIBLE CODE HERE. LARGELY BECAUSE OF HOW MUCH IVE BEEN BUILDING ON BASE CODE WITHOUT CHANGING THE OLD ONE AS I SHOULD.
    //BE MENTALLY PREPARED TO WITNESS HORRORS BEYOND COMPREHENSION IF YOU ATTEMPT TO WORK WITHIN THIS SCRIPT. GOOD LUCK AND MAY GOD BE WITH YOU.
    
    private bool leaningLeft = false;
    private bool leaningRight = false;
    private bool jumping = false;
    private bool ducking = false;

    private Vector3 newPos;
    private Vector3 oldPos;
    private Vector3 newJumpPos;
    private Vector3 oldJumpPos;
    private float LerpNumber;
    private float LerpJumpNumber;
    private CharacterController characterController;
    private bool canMove = true;
    private NavMeshAgent navMeshAgent;
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;
    [HideInInspector] public bool canTakeDamage = true;
    public float jumpheight = 2;
    public float speed = 0.02f;

    public ParticleSystem destroyFireParticle;
    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 30f;
    [FormerlySerializedAs("lerpSpeed")] public float dodgeSpeed = 5;
    public float jumpSpeed = 0.2f;
    private bool isMoving = false;
    private bool isJumping = false;
    private AnimatePlayer _animatePlayer;
    

    public GameObject youDiedUI;
    public ParticleSystem deathParticle;
    
    [Space]
    public AudioSource dodgeSound;
    public AudioSource gotHit;
    public AudioSource destroyFireSound;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        _animatePlayer =GetComponent<AnimatePlayer>();
        
        youDiedUI.SetActive(false);
        // navMeshAgent = GetComponent<NavMeshAgent>();
        // navMeshAgent.SetDestination(new Vector3(destination.transform.position.x, transform.position.y, transform.position.x));
    }


    private void FixedUpdate()
    {
        //Move smoothly
        if (isMoving)
        {
            // characterController.enabled = false;

            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(oldPos.z, newPos.z, LerpNumber));
            LerpNumber += dodgeSpeed  * Time.deltaTime;
            Physics.SyncTransforms();
            //     characterController.Move(new Vector3(0, 0, lerpValue));
            if (LerpNumber >= 1)
            {
                isMoving = false;
                if (-2 > transform.position.z  )
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -3);
                    Physics.SyncTransforms();
                }
                if (2 < transform.position.z  )
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 3);
                    Physics.SyncTransforms();
                }
            }
            
        }
        
        if (isJumping)
        {
            // characterController.enabled = false;
            jumping = true;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(oldJumpPos.y, newJumpPos.y, LerpJumpNumber), transform.position.z);
            LerpJumpNumber += jumpSpeed  * Time.deltaTime;
            Physics.SyncTransforms();
            //     characterController.Move(new Vector3(0, 0, lerpValue));
            if (LerpJumpNumber >= 1)
            {
                isJumping = false;
            }
        } 
    
    }

    void Update()
    {
        characterController.Move(new Vector3(-speed, 0, 0) * Time.deltaTime);
        Gravity();
//        FixPosition(); 
        ManageInputs();
        
       //  //Move smoothly
       //  if (isMoving)
       //  {
       //     // characterController.enabled = false;
       //
       //     transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(oldPos.z, newPos.z, LerpNumber));
       //      LerpNumber += dodgeSpeed  * Time.deltaTime;
       //      Physics.SyncTransforms();
       // //     characterController.Move(new Vector3(0, 0, lerpValue));
       //      if (LerpNumber >= 1)
       //      {
       //          isMoving = false;
       //          if (-6 > transform.position.z  )
       //          {
       //              transform.position = new Vector3(transform.position.x, transform.position.y, -7);
       //              Physics.SyncTransforms();
       //          }
       //          if (-4 < transform.position.z  )
       //          {
       //              transform.position = new Vector3(transform.position.x, transform.position.y, -3);
       //              Physics.SyncTransforms();
       //          }
       //      }
       //      
       //  }

        
        
        if (!characterController.isGrounded)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
        
        // if (isJumping)
        // {
        //     // characterController.enabled = false;
        //     jumping = true;
        //     transform.position = new Vector3(transform.position.x, Mathf.Lerp(oldJumpPos.y, newJumpPos.y, LerpJumpNumber), transform.position.z);
        //     LerpJumpNumber += jumpSpeed  * Time.deltaTime;
        //     Physics.SyncTransforms();
        //     //     characterController.Move(new Vector3(0, 0, lerpValue));
        //     if (LerpJumpNumber >= 1)
        //     {
        //         isJumping = false;
        //     }
        // } 
    }

    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            characterController.Move(new Vector3(0, -9, 0) * Time.deltaTime);
        }
    }

    private void FixPosition()
    {
        if (-4 > transform.position.z && -6 < transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
            Physics.SyncTransforms();
        }
    }

    private void ManageInputs()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
            }

            //Detects Swipe while finger is still moving on screen
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeAfterRelease)
                {
                    fingerDownPos = touch.position;
                    DetectSwipe();
                }
            }

            //Detects swipe after finger is released from screen
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPos = touch.position;
                DetectSwipe();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Duck();
        }
    }

    private void Duck()
    {
        if (!ducking && !jumping)
        {
            ducking = true;
            //appearance
            //transform.rotation = Quaternion.identity;
            //transform.localScale = new Vector3(1.3288f, 0.5f, 1.3288f);
            _animatePlayer.AnimateDuck();
            jumping = true;
           // StopAllCoroutines();
            StartCoroutine(ReturnToNormalPos());
        }
    }

    public void Jump()
    {
        // TODO maybe change so that when you jump while ducking or duck while jumping the first move gets cancelled
        if (!jumping && !ducking)
        {
            LerpJumpNumber = 0;
            jumping = true;
            //appearance
            transform.rotation = Quaternion.identity;
            _animatePlayer.AnimateJump();
            dodgeSound.Play();
            oldJumpPos = transform.position;
            newJumpPos = new Vector3(transform.position.x, transform.position.y + jumpheight, transform.position.z);
            isJumping = true;
           // transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
          //  characterController.Move(new Vector3(0, 2, 0f));
          //  StopAllCoroutines();
         //   StartCoroutine(ReturnToNormalPos());
        }
    }

    private void MoveRight()
    {
        if (canMove)
        {
            dodgeSound.Play();
            canMove = false;
            StartCoroutine(EnableMove());
                if (transform.position.z ! <= 2f)
                {
                    LerpNumber = 0;
                    isMoving = true;
                    oldPos = transform.position;
                    newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3);
                    //transform.position += new Vector3(0, 0, 1.9f);
                    // characterController.Move(new Vector3(0, 0, 2.000000f));

                }
                
            
        }
    }

  

    private void MoveLeft()
    {
        if (canMove)
        {
            dodgeSound.Play();
            canMove = false;
            StartCoroutine(EnableMove());
            if (transform.position.z !>= -2f)
            {   
                LerpNumber = 0;
                isMoving = true;
                oldPos = transform.position;
                newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
                //    Mathf.Lerp(0,2,)
                // transform.position += new Vector3(0, 0, -1.9f);
                //  characterController.Move(new Vector3(0, 0, -lerpValue));
            }
            
        }
    }
    private IEnumerator EnableMove()
    {
        yield return new WaitForSeconds(0.1f);
        canMove = true;
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
      Jump();
    }

    void OnSwipeDown ()
    {
      Duck();
    }

    void OnSwipeLeft ()
    {
       MoveLeft();
    }

    void OnSwipeRight ()
    {
        MoveRight();
    }
    
    private IEnumerator ReturnToNormalPos()
    {
        yield return new WaitForSeconds(0.4f);
       // transform.rotation = Quaternion.identity;
       // transform.position = new Vector3(transform.position.x, 1.17f, transform.position.z);
      //  transform.localScale = new Vector3(1.3288f, 1.3288f, 1.3288f)
        // jumping = false;
        ducking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slope"))
        {
            isMoving = false;
        }
            //TODO fix dodging tags for new obstacle types.
        //manage dodging
        if (other.CompareTag("MiddleObstacle"))
        {
            GotHit(other.gameObject);
            DebugShit(other.gameObject);
            

        }
        if (other.CompareTag("LowObstacle"))
        {
            if (!jumping)
            {
                GotHit(other.gameObject);
                DebugShit(other.gameObject);
            }
        }
        if (other.CompareTag("HighObstacle"))
        {
            if (!ducking)
            {
                GotHit(other.gameObject);
                DebugShit(other.gameObject);
            }   
        }
    }

    private void GotHit(GameObject fire)
    {

        if (canTakeDamage)
        {
            //SceneManager.LoadScene(0);  
            youDiedUI.SetActive(true);
            Instantiate(deathParticle, transform.position, quaternion.identity);
            Instantiate(gotHit);
            GameObject dragon =  FindObjectOfType<Dragon>().gameObject;
            dragon.GetComponent<Animator>().SetTrigger("PlayerDied");
            dragon.GetComponent<Dragon>().enabled = false;
            Destroy(gameObject);
        }
        else
        {
            Destroy(fire);
            destroyFireParticle.Play();
            destroyFireSound.Play();
        }
    }

    private void DebugShit(GameObject obstacle)
    {
        //      print(obstacle);
    }
    
    
    
}
