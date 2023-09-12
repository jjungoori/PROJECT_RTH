using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public List<Vector3> spawnPositions = new List<Vector3>(); // List to hold spawn positions
    public Vector2 noteXY = new Vector2(0, 0); // The number of spawn points
    public Vector2 offsetXY = new Vector2(0, 0);
    public Transform anchor;
    Vector3 _anchorPosition;

    private void Awake()
    {
        _anchorPosition = anchor.position;

        for (int i = 0; i < noteXY.x; i++) // Start from 1, as 0 is already added
        {
            for (int l = 0; l < noteXY.y; l++)
            {
                // Calculate the position for this spawn point. This is just an example; you can set it however you like.
                Vector3 spawnPosition = _anchorPosition + new Vector3(i * offsetXY.x, l * offsetXY.y, 0);

                // Add the position to the list
                spawnPositions.Add(spawnPosition);
            }
        }
    }

}