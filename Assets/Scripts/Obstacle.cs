using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 15f;
    public bool isMoving = true;
    private MeshRenderer meshRenderer;


    private AudioSource shootSound;
    // Start is called before the first frame update
    void Start()
    {
        shootSound = GetComponent<AudioSource>();
       meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (meshRenderer.enabled && isMoving)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ShowObstacles")
        {
            shootSound.Play();
            meshRenderer.enabled = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
