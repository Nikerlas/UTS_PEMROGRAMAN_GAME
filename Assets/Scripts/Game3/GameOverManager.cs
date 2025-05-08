using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject panelGameOver;

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
}
