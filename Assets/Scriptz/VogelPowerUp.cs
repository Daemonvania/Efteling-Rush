using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VogelPowerUp : MonoBehaviour
{
    public GameObject Vogel;

    public CinemachineVirtualCamera cinemachineVirtualCamera;


    private GameObject[] EndVogels;

    // Start is called before the first frame update
    void Start()
    {
        Vogel.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vogel"))
        {
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
            cinemachineVirtualCamera.m_Lens.FieldOfView = 80;
            Physics.SyncTransforms();
            Vogel.SetActive(true);

            FindClosestEnd().GetComponent<EndVogel>().initDist = transform.position.x - FindClosestEnd().transform.position.x;
            FindClosestEnd().GetComponent<EndVogel>().EnableDuration(true);
        }

        if (other.CompareTag("EndVogel"))
            {
                Vogel.SetActive(false);
                FindClosestEnd().GetComponent<EndVogel>().EnableDuration(false);
                cinemachineVirtualCamera.m_Lens.FieldOfView = 40;
            }
        

    }

    GameObject FindClosestEnd()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        EndVogels = GameObject.FindGameObjectsWithTag("EndVogel");

        foreach (GameObject endVogel in EndVogels)
        {
            Vector3 diff = endVogel.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = endVogel;
                distance = curDistance;
            }

            
        }
        return closest.gameObject;
    }
}
