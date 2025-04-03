using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<SegmentData> allSegments; // All available segments
    private List<SegmentData> shuffledSegments; // Shuffled version
    private int currentSegmentIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ShuffleSegments();
        LoadNextSegment();
    }

    // 🔀 Shuffle the segments randomly
    private void ShuffleSegments()
    {
        shuffledSegments = new List<SegmentData>(allSegments);
        for (int i = 0; i < shuffledSegments.Count; i++)
        {
            SegmentData temp = shuffledSegments[i];
            int randomIndex = Random.Range(i, shuffledSegments.Count);
            shuffledSegments[i] = shuffledSegments[randomIndex];
            shuffledSegments[randomIndex] = temp;
        }
    }

    public void LoadNextSegment()
    {
        if (currentSegmentIndex < shuffledSegments.Count)
        {
            FindObjectOfType<SegmentUI>().SetSegment(shuffledSegments[currentSegmentIndex]);
            currentSegmentIndex++;
        }
        else
        {
            Debug.Log("All segments completed!");
            // Handle end of game here
        }
    }
}
