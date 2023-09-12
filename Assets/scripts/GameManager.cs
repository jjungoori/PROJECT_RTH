using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject notePrefab;
    public int[] noteSampleTimes;
    public int timingThresholdInSamples = 22050;
    private List<Note> activeNotes = new List<Note>();
    private int nextNoteIndex = 0;

    public SpawnArea spawnArea;

    [System.Serializable]
    public class Note
    {
        public GameObject gameObject;
        public int targetHitSample;
        public bool colorChanged = false;
    }

    private void Start()
    {
        int numberOfNotes = Mathf.Min(spawnArea.spawnPositions.Count, 200);
        noteSampleTimes = new int[numberOfNotes];
        int currentSample = 0;
        int sampleRate = musicSource.clip.frequency;
        float beatsPerMinute = 268; // BPM 값을 설정합니다. 필요에 따라 이 값을 변경할 수 있습니다.
        float beatInterval = 60f / beatsPerMinute; // 한 박자의 시간 간격을 계산합니다.

        for (int i = 0; i < numberOfNotes; i++)
        {
            currentSample += Mathf.FloorToInt(beatInterval * sampleRate);  // 한 박자마다 샘플을 설정합니다.
            noteSampleTimes[i] = currentSample;

            Vector3 localSpawnPos = spawnArea.spawnPositions[i];
            // Transform the local spawn position to world position based on the spawnArea's position and rotation
            //Vector3 spawnPos = spawnArea.transform.TransformPoint(localSpawnPos);
    
            GameObject spawnedNote = Instantiate(notePrefab, localSpawnPos, Quaternion.identity);
            spawnedNote.transform.SetParent(spawnArea.transform, true);
            activeNotes.Add(new Note { gameObject = spawnedNote, targetHitSample = currentSample });
        }
    }





    private void Update()
    {
        int currentSampleTime = musicSource.timeSamples;

        foreach (Note note in activeNotes)
        {
            if (!note.colorChanged && currentSampleTime >= note.targetHitSample)
            {
                // note.gameObject.SetActive(true);  // Activate the note
                note.gameObject.GetComponent<Renderer>().material.color = Color.red;  // Change color to red
                note.colorChanged = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForHit();
        }
    }

    private void CheckForHit()
    {
        int currentSampleTime = musicSource.timeSamples;
        Note closestNote = null;
        int closestSampleDifference = timingThresholdInSamples;

        foreach (Note note in activeNotes)
        {
            int sampleDifference = Mathf.Abs(currentSampleTime - note.targetHitSample);

            if (sampleDifference < closestSampleDifference)
            {
                closestNote = note;
                closestSampleDifference = sampleDifference;
            }
        }

        if (closestNote != null)
        {
            //Destroy(closestNote.gameObject);
            closestNote.gameObject.GetComponent<Renderer>().material.color = Color.green;
            activeNotes.Remove(closestNote);
            // Update score, combo, etc.
        }
        else
        {
            // The player missed, decrease health, reset combo, etc.
        }
    }
}
