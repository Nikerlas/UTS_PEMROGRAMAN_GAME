using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectArena : MonoBehaviour
{
    public ArenaData[] arenaList;
    public Image arenaPreview;
    public Text arenaNameText;
    public Text difficultyText; // ⬅️ Tambahkan untuk tampilkan info difficulty (optional)
    public Button prevButton, nextButton, startButton;

    private int currentIndex = 0;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) NextArena();
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) PrevArena();
    }

    public void NextArena()
    {
        Debug.Log(arenaList.Length);
        if (currentIndex < arenaList.Length - 1)
        {
            currentIndex++;
            Debug.Log(currentIndex);
            UpdateUI();
        }
    }

    public void PrevArena()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Debug.Log(currentIndex);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        ArenaData data = arenaList[currentIndex];
        arenaPreview.sprite = data.previewImage;
        arenaNameText.text = data.namaArena;

        // ⬇️ Menampilkan difficulty arena (jika kamu tambahkan UI Text-nya)
        if (difficultyText != null)
        {
            difficultyText.text = data.difficulty.ToString();
        }

        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < arenaList.Length - 1;
    }

    public void StartGame()
    {
        ArenaInfo.selectedArena = arenaList[currentIndex];
        ArenaInfo.selectedDifficulty = arenaList[currentIndex].difficulty;
        SceneManager.LoadScene("Game2");
    }
}
