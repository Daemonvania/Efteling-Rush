using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkSystem : MonoBehaviour
{
    public GameObject[] chunks;
    public GameObject[] chunkPosIndicators;
    void Start()
    {
        
        foreach (GameObject chunkPos in chunkPosIndicators)
        {
            GameObject selection = chunks[Random.Range(0, chunks.Length)];
            Vector3 pos = chunkPos.transform.position;

            Instantiate(selection, pos, Quaternion.identity);

        //    chunkPosIndicators.Remove(selection);
        }
        
        // foreach (GameObject chunk in chunks)
        // {
        //     GameObject selection = chunkPosIndicators[Random.Range(0, chunkPosIndicators.Count - 1)];
        //     Vector3 randomPos = selection.transform.position;
        //
        //     Instantiate(chunk, randomPos, Quaternion.identity);
        //
        //     chunkPosIndicators.Remove(selection);
        // }
    }
}