using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public SegmentUI segmentUI;          // Reference to the UI manager
    public List<SegmentData> segments;   // List of all segments
    private int currentSegmentIndex = 0; // Tracks the current segment

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Keep GameManager alive across scenes
    }

    private void Start()
    {
        LoadNextSegment(); // Load the first segment at startup
    }

    public void LoadNextSegment()
    {
        if (currentSegmentIndex < segments.Count)
        {
            segmentUI.SetSegment(segments[currentSegmentIndex]);
            currentSegmentIndex++;
        }
        else
        {
            Debug.Log("Game Over! All segments completed.");
            // You can add a results screen or reset logic here
        }
    }
}
