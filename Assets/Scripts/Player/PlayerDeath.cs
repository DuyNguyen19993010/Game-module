using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{

    void OnDestroy()
    {
        //Go to respawn scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
}
