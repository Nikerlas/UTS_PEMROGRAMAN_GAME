using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject panelGameOver;
    private static GameOverManager instance;
    
    void Awake()
    {
        instance = this;
        if (panelGameOver != null)
            panelGameOver.SetActive(false);
    }

    public static void TriggerGameOver()
    {
        if (instance == null || instance.panelGameOver == null) return;

        instance.panelGameOver.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Over!");
    }
    public void TampilkanGameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0f; // menghentikan permainan
    }

    public void MulaiUlang()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void KembaliKeMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // Ganti dengan nama scene menu utama Anda
    }
}
