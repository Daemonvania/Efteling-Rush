using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkSystem : MonoBehaviour
{
    public GameObject[] chunks;
    public List<GameObject> chunkPosIndicators = new List<GameObject>();
    void Start()
    {
        foreach (GameObject chunk in chunks)
        {
            GameObject selection = chunkPosIndicators[Random.Range(0, chunkPosIndicators.Count - 1)];
            Vector3 randomPos = selection.transform.position;

            Instantiate(chunk, randomPos, Quaternion.identity);

            chunkPosIndicators.Remove(selection);
        }
    }
}