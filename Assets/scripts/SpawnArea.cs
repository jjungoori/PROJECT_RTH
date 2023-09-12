using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public List<Vector3> spawnPositions = new List<Vector3>(); // List to hold spawn positions
    public int numberOfSpawnPoints = 80; // The number of spawn points

    private void Awake()
    {
        for (int i = 0; i < numberOfSpawnPoints; i++)
        {
            // Calculate the position for this spawn point. This is just an example; you can set it however you like.
            Vector3 spawnPosition = new Vector3(i * 2, 0, 0);

            // Add the position to the list
            spawnPositions.Add(spawnPosition);
        }
    }
}