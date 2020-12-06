using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("value", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ContinueGame()
    {
        PlayerPrefs.SetInt("value", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
