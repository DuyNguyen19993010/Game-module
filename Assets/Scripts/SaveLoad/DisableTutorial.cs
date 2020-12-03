using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTutorial : MonoBehaviour
{
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
