using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject notePrefab; // Reference to your note prefab
    public float[] noteTimes; // The times at which notes should spawn
    public float timingThreshold = 0.2f; // The timing tolerance for hitting a note
    private float customTimer = 0f; // Your custom timer
    private List<Note> activeNotes = new List<Note>(); // List of active notes
    private int nextNoteIndex = 0; // Index of the next note to spawn

    [System.Serializable]
    public class Note
    {
        public GameObject gameObject;
        public float targetHitTime;
    }

    private void Update()
    {
        // Increment the custom timer
        customTimer += Time.deltaTime;

        // Check if it's time to spawn the next note
        if (nextNoteIndex < noteTimes.Length && customTimer >= noteTimes[nextNoteIndex])
        {
            // Spawn the note and add it to the list of active notes
            GameObject spawnedNote = Instantiate(notePrefab, new Vector3(10, 0, 0), Quaternion.identity);
            activeNotes.Add(new Note { gameObject = spawnedNote, targetHitTime = noteTimes[nextNoteIndex] });
            
            // Move to the next note
            nextNoteIndex++;
        }

        // Your existing input check and hit detection logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForHit();
        }
    }

    private void CheckForHit()
    {
        float currentTime = musicSource.time;
        Note closestNote = null;
        float closestTimeDifference = timingThreshold;

        foreach (Note note in activeNotes)
        {
            float timeDifference = Mathf.Abs(currentTime - note.targetHitTime);

            if (timeDifference < closestTimeDifference)
            {
                closestNote = note;
                closestTimeDifference = timeDifference;
            }
        }

        if (closestNote != null)
        {
            // The player hit the note within the allowed time window
            Destroy(closestNote.gameObject);
            activeNotes.Remove(closestNote);

            // Update score, combo, etc.
        }
        else
        {
            // The player missed
            // Decrease health, reset combo, etc.
        }
    }
}