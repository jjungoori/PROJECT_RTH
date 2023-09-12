using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public List<Transform> spawnPositions = new List<Transform>(); // List to hold spawn positions

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            spawnPositions.Add();
        }
    }
}