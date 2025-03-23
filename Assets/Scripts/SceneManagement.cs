using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneManagement : MonoBehaviour
{
    public string EnterScene;
    public string ExitScene;
    public bool isEscapeToExit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(isEscapeToExit)
            {
                Application.Quit();
            }
            else
            {
                BackToMenu();
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
