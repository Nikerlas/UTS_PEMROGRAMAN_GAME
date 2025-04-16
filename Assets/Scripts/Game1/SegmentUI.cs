using UnityEngine;
using UnityEngine.UI;

public class SegmentUI : MonoBehaviour
{
    public Image[] flagButtons;          // Image components for flag choices
    public Button[] flagButtonRefs;      // Button components for interactivity
    public Button speakerButton;         // Button to play audio
    public AudioSource audioSource;      // AudioSource for greeting audio
    public AudioSource sfxSource;        // AudioSource for playing SFX (one-shot)
    public AudioClip correctSFX;         // Sound effect for a correct answer
    public AudioClip wrongSFX;           // Sound effect for a wrong answer
    public Text scoreText;               // Score display
    public Button nextButton;            // Next segment button (Initially hidden)

    private string correctAnswer;        // Stores the correct flag name
    private int score = 0;               // Player's score
    private int streak = 0;              // Correct answer streak

    // 🔹 Exit Popup UI Components (Optional)
    public GameObject exitPopup;
    public Button btnYes;
    public Button btnNo;

    void Start()
    {
        scoreText.text = "Score: " + score; // Show score on start
        nextButton.gameObject.SetActive(false);
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

            // Reset the flag image color
            flagButtons[i].color = Color.white;
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
        // Disable all buttons to prevent multiple answers
        foreach (Button btn in flagButtonRefs)
        {
            btn.interactable = false;
        }

        bool isCorrect = selectedFlagName == correctAnswer;

        for (int i = 0; i < flagButtons.Length; i++)
        {
            string btnName = flagButtons[i].sprite.name;

            if (btnName == correctAnswer)
            {
                flagButtons[i].color = Color.green;
                Debug.Log($"✅ Flag '{btnName}' turned GREEN (Correct)");
            }

            if (btnName == selectedFlagName && !isCorrect)
            {
                flagButtons[i].color = Color.red;
                Debug.Log($"❌ Flag '{btnName}' turned RED (Wrong)");
            }
        }

        // Scoring
        if (isCorrect)
        {
            Debug.Log("Correct! 🎉");
            score += 10;
            streak++;
            if (streak >= 3)
            {
                Debug.Log("🔥 Streak bonus!");
                score += 5;
            }

            if (correctSFX != null)
                sfxSource.PlayOneShot(correctSFX);
        }
        else
        {
            Debug.Log("Wrong! ❌");
            streak = 0;

            if (wrongSFX != null)
                sfxSource.PlayOneShot(wrongSFX);
        }

        scoreText.text = "Score: " + score;
        nextButton.gameObject.SetActive(true); // Show next button
    }

    public void NextSegment()
    {
        GameManager.Instance.LoadNextSegment();
    }
}
