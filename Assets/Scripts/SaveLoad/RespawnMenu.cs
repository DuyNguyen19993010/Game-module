using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnMenu : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1;
    }
    public void Respawn()
    {
        PlayerPrefs.SetInt("value", 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
