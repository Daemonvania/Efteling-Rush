using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
       
    }

    private void Update()
    {
        transform.position += new Vector3(-player.speed, 0, 0) * Time.deltaTime;
    }
    
    
}
