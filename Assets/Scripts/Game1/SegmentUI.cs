using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SegmentUI : MonoBehaviour
{
    public Image[] flagButtons;
    public Button[] flagButtonRefs;
    public Button speakerButton;
    public AudioSource audioSource;
    public AudioSource sfxSource;
    public AudioClip correctSFX;
    public AudioClip wrongSFX;
    public Text scoreText;
    public Button nextButton;

    private string correctAnswer;
    private int score = 0;
    private int streak = 0;

    // Exit popup
    public GameObject exitPopup;
    public Button btnYes;
    public Button btnNo;

    void Start()
    {
        scoreText.text = "Score: " + score; // 👈 Make score visible at start
    }

    public void SetSegment(SegmentData segment)
    {
        correctAnswer = segment.correctFlagName;
        audioSource.clip = segment.greetingAudio;

        for (int i = 0; i < flagButtons.Length; i++)
        {
            flagButtons[i].sprite = segment.flagChoices[i];

            string flagName = segment.flagChoices[i].name;
            flagButtonRefs[i].onClick.RemoveAllListeners();
            flagButtonRefs[i].onClick.AddListener(() => CheckAnswer(flagName));

            flagButtonRefs[i].interactable = true;
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
        foreach (Button btn in flagButtonRefs)
        {
            btn.interactable = false;
        }

        if (selectedFlagName == correctAnswer)
        {
            Debug.Log("Correct! 🎉");
            streak++;
            int bonus = streak >= 3 ? 5 : 0; // 👈 Streak bonus after 3 correct answers
            score += 10 + bonus;

            if (bonus > 0)
            {
                Debug.Log($"Streak bonus! 🔥 +{bonus}");
            }

            if (correctSFX != null)
            {
                sfxSource.PlayOneShot(correctSFX);
            }
        }
        else
        {
            Debug.Log("Wrong! ❌");
            streak = 0; // 👈 Reset streak
            // No point deduction
            if (wrongSFX != null)
            {
                sfxSource.PlayOneShot(wrongSFX);
            }
        }

        scoreText.text = "Score: " + score;
        nextButton.gameObject.SetActive(true);
    }

    public void NextSegment()
    {
        GameManager.Instance.LoadNextSegment();
    }
}
