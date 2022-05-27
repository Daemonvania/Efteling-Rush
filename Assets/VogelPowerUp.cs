using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VogelPowerUp : MonoBehaviour
{
    public GameObject Vogel;

    public CinemachineVirtualCamera cinemachineVirtualCamera;
    
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
            transform.position = new Vector3(transform.position.x, 10,  transform.position.z);
            cinemachineVirtualCamera.m_Lens.FieldOfView = 100;
            Physics.SyncTransforms();
            Vogel.SetActive(true);
        }

        if (other.CompareTag("EndVogel"))
        {
            Vogel.SetActive(false);
            cinemachineVirtualCamera.m_Lens.FieldOfView = 40;
        }
    }
    
}
