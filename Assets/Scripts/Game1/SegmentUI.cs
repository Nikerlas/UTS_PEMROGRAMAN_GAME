using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FlagChoice
{
    public Button button;
    public Image flagImage;
    public Image highlightImage;
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
    public GameObject funFactPanel;
    public Text funFactText;
    public Button funFactNextButton;
    public GameObject gameOverPanel;
    public Text finalScoreText;

    private string correctAnswer;
    private int score = 0;
    private int streak = 0;

    private SegmentData currentSegment;

    void Start()
    {
        scoreText.text = "Score: 0";

        if (funFactNextButton != null)
        {
            funFactNextButton.onClick.AddListener(OnClickNextFromFunFact);
        }

        if (funFactPanel != null)
        {
            funFactPanel.SetActive(false);
        }
    }

    public void SetSegment(SegmentData segment)
    {
        currentSegment = segment;
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
                score += 5;
            }
            sfxSource.PlayOneShot(correctSFX);
        }
        else
        {
            streak = 0;
            sfxSource.PlayOneShot(wrongSFX);
        }

        scoreText.text = "Score: " + score;

        ShowFunFact(currentSegment.funFact);
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

        GameManager.Instance.LoadNextSegment();
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
