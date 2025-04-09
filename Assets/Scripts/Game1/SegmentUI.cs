using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SegmentUI : MonoBehaviour
{
    public Image[] flagButtons;      // Image components for flag choices
    public Button[] flagButtonRefs;  // Button components for interactivity
    public Button speakerButton;     // Button to play audio
    public AudioSource audioSource;     // AudioSource for greeting audio
    public AudioSource sfxSource;       // AudioSource for playing SFX (one-shot)
    public AudioClip correctSFX;        // Sound effect for a correct answer
    public AudioClip wrongSFX;          // Sound effect for a wrong answer
    public Text scoreText;              // Score display
    public Button nextButton;           // Next segment button (Initially hidden)

    private string correctAnswer;       // Stores the correct flag name
    private int score = 0;              // Player's score

    // 🔹 Exit Popup UI Components
    public GameObject exitPopup;        // Panel popup exit
    public Button btnYes;               // Tombol untuk keluar
    public Button btnNo;                // Tombol untuk membatalkan exit

    void Start()
    {
        // Sembunyikan popup exit di awal
        exitPopup.SetActive(false);

        // Tambahkan event listener untuk tombol Exit
        btnYes.onClick.AddListener(ExitGame);
        btnNo.onClick.AddListener(CloseExitPopup);
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

            // ✅ Re-enable buttons when a new segment loads
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
        // Disable all buttons to prevent multiple answers
        foreach (Button btn in flagButtonRefs)
        {
            btn.interactable = false;
        }

        // Play correct or wrong SFX
        if (selectedFlagName == correctAnswer)
        {
            Debug.Log("Correct! 🎉");
            score += 10;
            if (correctSFX != null)
            {
                sfxSource.PlayOneShot(correctSFX);
            }
        }
        else
        {
            Debug.Log("Wrong! ❌");
            score -= 5;
            if (wrongSFX != null)
            {
                sfxSource.PlayOneShot(wrongSFX);
            }
        }

        scoreText.text = "Score: " + score;
        nextButton.gameObject.SetActive(true); // Show next button
    }
    public void NextSegment()
    {
        // Load the next question
        GameManager.Instance.LoadNextSegment();
    }


}
