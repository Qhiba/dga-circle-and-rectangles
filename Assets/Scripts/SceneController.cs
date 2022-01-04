using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance = null;
    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneController>();
            }

            return _instance;
        }
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            GameManager.Instance.isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            GameManager.Instance.isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MainGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ProblemList()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
