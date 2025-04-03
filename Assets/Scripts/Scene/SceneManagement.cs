using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    public string EscapeScene;
    public bool isEscapeForExit = false;
    public GameObject exitPopup; // Reference to the exit confirmation popup

    void Start()
    {
        // Only hide exitPopup if it exists in this scene
        if (exitPopup != null)
        {
            exitPopup.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If exitPopup exists and is open, close it
            if (exitPopup != null && exitPopup.activeSelf) 
            {
                CancelExit();
            }
            else if (isEscapeForExit)
            {
                // Show exit popup only if it exists
                if (exitPopup != null) 
                {
                    ShowExitPopup();
                }
                else
                {
                    Application.Quit();
                    Debug.Log("Game is exiting");
                }
            }
            else if (!string.IsNullOrEmpty(EscapeScene))
            {
                SceneManager.LoadScene(EscapeScene); // Switch scene if EscapeScene is set
            }
        }
    }

    public void SelectGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("Credit");
    }

    public void ExitGame()
    {
        if (exitPopup != null)
        {
            ShowExitPopup();
        }
        else
        {
            Application.Quit();
            Debug.Log("Game is exiting");
        }
    }

    void ShowExitPopup()
    {
        if (exitPopup != null)
        {
            exitPopup.SetActive(true);
        }
    }

    public void ConfirmExit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void CancelExit()
    {
        if (exitPopup != null)
        {
            exitPopup.SetActive(false);
        }
    }
}
