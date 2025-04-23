using UnityEngine;
using UnityEngine.UI;

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
    public FlagChoice[] flagChoices;

    [Header("Audio")]
    public Button speakerButton;
    public AudioSource audioSource;
    public AudioSource sfxSource;
    public AudioClip correctSFX;
    public AudioClip wrongSFX;

    [Header("UI")]
    public Text scoreText;
    public GameObject funFactPanel;       // Panel untuk menampilkan fun fact
    public Text funFactText;              // Teks fun fact
    public Button funFactNextButton;      // Tombol Next di panel fun fact
    public GameObject gameOverPanel;      // Panel game selesai
    public Text finalScoreText;           // Menampilkan skor akhir

    private string correctAnswer;
    private int score = 0;
    private int streak = 0;

    public void Start()
    {
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: 0";

        if (funFactNextButton != null)
        {
            funFactNextButton.onClick.AddListener(OnClickNextFromFunFact);
        }
    }

    public void SetSegment(SegmentData segment)
    {
        correctAnswer = segment.correctFlagName;
        audioSource.clip = segment.greetingAudio;

        for (int i = 0; i < flagChoices.Length; i++)
        {
            flagChoices[i].flagImage.sprite = segment.flagChoices[i];
            flagChoices[i].highlightImage.color = Color.clear;

            string flagName = segment.flagChoices[i].name;

            flagChoices[i].button.onClick.RemoveAllListeners();
            flagChoices[i].button.onClick.AddListener(() => CheckAnswer(flagName));
            flagChoices[i].button.interactable = true;
        }

        // Sembunyikan panel fun fact
        if (funFactPanel != null)
        {
            funFactPanel.SetActive(false);
        }
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
            }
            else if (flagName == selectedFlagName)
            {
                flagChoices[i].highlightImage.color = Color.red;
            }
        }

        if (isCorrect)
        {
            score += 10;
            streak++;
            if (streak > 1)
            {
                score += 5; // bonus streak
            }

            sfxSource.PlayOneShot(correctSFX);
        }
        else
        {
            streak = 0;
            sfxSource.PlayOneShot(wrongSFX);
        }

        scoreText.text = "Score: " + score;

        // Tampilkan fun fact
        ShowFunFact("Fun Fact: Sapaan ini berasal dari " + correctAnswer + "!");
    }

    void ShowFunFact(string fact)
    {
        if (funFactPanel != null)
        {
            funFactText.text = fact;
            funFactPanel.SetActive(true);
        }
    }

    public void OnClickNextFromFunFact()
    {
        if (funFactPanel != null)
        {
            funFactPanel.SetActive(false);
        }
    }

    public void ShowFinishPopup()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score Akhir: " + score;
        }
    }
}
