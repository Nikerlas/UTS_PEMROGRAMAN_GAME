using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data Segmen")]
    public List<SegmentData> allSegments; // Semua segmen tersedia
    private List<SegmentData> shuffledSegments; // Daftar segmen yang sudah diacak
    private int currentSegmentIndex = 0;

    [Header("UI Handler")]
    public SegmentUI segmentUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (shuffledSegments == null || shuffledSegments.Count == 0)
        {
            ShuffleSegments();
        }

        if (segmentUI == null)
        {
            segmentUI = FindObjectOfType<SegmentUI>();
        }

        if (shuffledSegments.Count > 0)
        {
            // Langsung panggil LoadSegment tanpa menaikkan indeks
            LoadSegment(currentSegmentIndex);
        }
        else
        {
            Debug.LogError("❌ Tidak ada segmen dalam daftar!");
        }
    }


    // 🔀 Mengacak daftar segmen
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

    // Memuat segmen berdasarkan indeks tertentu dari daftar yang sudah diacak
    public void LoadSegment(int index)
    {
        if (index >= 0 && index < shuffledSegments.Count)
        {
            segmentUI.SetSegment(shuffledSegments[index]);
        }
        else
        {
            Debug.LogWarning("⚠️ Indeks segmen tidak valid, menampilkan Game Over!");
            EndGame();
        }
    }

    // Memuat segmen berikutnya
    public void LoadNextSegment()
    {
        currentSegmentIndex++;

        if (currentSegmentIndex < shuffledSegments.Count)
        {
            LoadSegment(currentSegmentIndex);
        }
        else
        {
            EndGame();
        }
    }

    // Menangani akhir permainan
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
        ShuffleSegments(); // Acak ulang segmen saat restart
        Time.timeScale = 1f;
        LoadSegment(currentSegmentIndex);
        segmentUI.Start(); // Sembunyikan popup selesai
    }
}
