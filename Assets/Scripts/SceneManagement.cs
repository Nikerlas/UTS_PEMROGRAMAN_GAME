using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneManagement : MonoBehaviour
{
    public void PilihPermainan()
    {
        SceneManager.LoadScene("Level");
    }
    public void KembaliKeMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void CreditScene()
    {
        SceneManager.LoadScene("Credit");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
