using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    private void Update()
    {
        transform.position += new Vector3(-0.02f, 0, 0);
    }
}
