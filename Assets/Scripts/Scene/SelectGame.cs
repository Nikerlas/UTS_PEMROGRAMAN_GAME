using UnityEngine;
using UnityEngine.UI;

public class SelectGame : MonoBehaviour
{
    public GameObject[] gameButtons; // Array of GameObjects (your designed buttons)
    public Button prevButton, nextButton, secretGame; // Prev & Next navigation buttons

    private int currentIndex = 0; // Track active game index

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            NextGame();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PrevGame();
        }
    }

    public void NextGame()
    {
        if (currentIndex < gameButtons.Length - 1)
        {
            currentIndex++;
            UpdateUI();
        }
    }

    public void PrevGame()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        // Activate only the current game button
        for (int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i].SetActive(i == currentIndex);
        }

        // Enable/disable navigation buttons
        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < gameButtons.Length - 1;
        secretGame.interactable = false;
    }
}
