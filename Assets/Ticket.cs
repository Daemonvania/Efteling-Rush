using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 130 * Time.deltaTime, 0, Space.World);
    }
}
