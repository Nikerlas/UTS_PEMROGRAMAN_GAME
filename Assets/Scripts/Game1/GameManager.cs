using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data Segmen")]
    public List<SegmentData> segments;        // Daftar semua soal
    private int currentSegmentIndex = 0;

    [Header("UI Handler")]
    public SegmentUI segmentUI;               // Referensi ke SegmentUI

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opsional jika ingin bertahan lintas scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (segments.Count > 0)
        {
            LoadSegment(currentSegmentIndex);
        }
        else
        {
            Debug.LogError("❌ Tidak ada segmen dalam daftar!");
        }
    }

    public void LoadSegment(int index)
    {
        if (index >= 0 && index < segments.Count)
        {
            segmentUI.SetSegment(segments[index]);
        }
        else
        {
            Debug.LogWarning("⚠️ Indeks segmen tidak valid, menampilkan Game Over!");
            EndGame();
        }
    }

    public void LoadNextSegment()
    {
        currentSegmentIndex++;

        if (currentSegmentIndex < segments.Count)
        {
            LoadSegment(currentSegmentIndex);
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        if (segmentUI != null)
        {
            segmentUI.ShowFinishPopup();
        }
        else
        {
            Debug.LogError("❌ SegmentUI belum diassign ke GameManager.");
        }
    }

    // (Opsional) Reset untuk restart game
    public void RestartGame()
    {
        currentSegmentIndex = 0;
        Time.timeScale = 1f;
        LoadSegment(currentSegmentIndex);
    }
}
