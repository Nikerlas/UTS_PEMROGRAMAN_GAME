using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class FlagChoice
{
    public Button button;         // Tombol untuk klik
    public Image flagImage;       // Gambar benderanya
    public Image highlightImage;  // Panel warna di belakang bendera
}

public class SegmentUI : MonoBehaviour
{
    [Header("Flag Choices")]
    public FlagChoice[] flagChoices;      // FlagChoice array (button + flag image + highlight)

    [Header("Audio")]
    public Button speakerButton;
    public AudioSource audioSource;
    public AudioSource sfxSource;
    public AudioClip correctSFX;
    public AudioClip wrongSFX;

    [Header("UI")]
    public Text scoreText;
    public Button nextButton;

    [Header("Exit Popup")]
    public GameObject exitPopup;
    public Button btnYes;
    public Button btnNo;

    private string correctAnswer;
    private int score = 0;
    private int streak = 0;

    void Start()
    {
        scoreText.text = "Score: 0";
    }

    public void SetSegment(SegmentData segment)
    {
        correctAnswer = segment.correctFlagName;
        audioSource.clip = segment.greetingAudio;

        for (int i = 0; i < flagChoices.Length; i++)
        {
            // Set gambar bendera
            flagChoices[i].flagImage.sprite = segment.flagChoices[i];

            // Reset warna highlight
            flagChoices[i].highlightImage.color = Color.clear;

            string flagName = segment.flagChoices[i].name;

            // Clear listener sebelumnya
            flagChoices[i].button.onClick.RemoveAllListeners();

            // Tambahkan listener baru
            flagChoices[i].button.onClick.AddListener(() => CheckAnswer(flagName));

            // Aktifkan lagi tombolnya
            flagChoices[i].button.interactable = true;
        }

        nextButton.gameObject.SetActive(false);
    }

    public void PlayGreeting()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public void CheckAnswer(string selectedFlagName)
    {
        // Disable semua tombol
        foreach (var choice in flagChoices)
        {
            choice.button.interactable = false;
        }

        bool isCorrect = selectedFlagName == correctAnswer;

        for (int i = 0; i < flagChoices.Length; i++)
        {
            string flagName = flagChoices[i].flagImage.sprite.name;

            if (flagName == correctAnswer)
            {
                flagChoices[i].highlightImage.color = Color.green;
                Debug.Log("Correct highlight color applied.");
            }
            else if (flagName == selectedFlagName)
            {
                flagChoices[i].highlightImage.color = Color.red;
                Debug.Log("Wrong highlight color applied.");
            }
        }

        if (isCorrect)
        {
            score += 10;
            streak += 1;
            if (streak > 1)
            {
                score += 5; // bonus streak
                Debug.Log($"Streak bonus! Streak: {streak}");
            }

            sfxSource.PlayOneShot(correctSFX);
        }
        else
        {
            streak = 0;
            sfxSource.PlayOneShot(wrongSFX);
        }

        scoreText.text = "Score: " + score;
        nextButton.gameObject.SetActive(true);
    }

    public void NextSegment()
    {
        GameManager.Instance.LoadNextSegment();
    }
}
