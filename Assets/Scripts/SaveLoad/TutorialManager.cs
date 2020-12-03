using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Transform tutorial_1_position;
    public Transform tutorial_2_position;
    public Transform tutorial_3_position;
    public bool tutorial_1_done;
    public bool tutorial_2_done;
    public bool tutorial_3_done;
    public GameObject MovementAttackTutorial;
    public GameObject healthSpiritBarTutorial;
    public GameObject ItemTutorial;
    //--------------Player-----------------
    public GameObject player;
    void OnEnable()
    {
        tutorial_1_done = false;
        tutorial_2_done = false;
        tutorial_3_done = false;
    }
    void Update()
    {
        if (!tutorial_1_done)
        {
            if (player.transform.position.x >= tutorial_1_position.position.x)
            {
                ActivateTutorial(1);

            }
        }
        else
        {
            DeactivateTutorial(1);
        }
        if (!tutorial_2_done)
        {
            if (player.transform.position.x >= tutorial_2_position.position.x)
            {
                ActivateTutorial(2);

            }

        }
        else
        {
            DeactivateTutorial(2);
        }

        if (!tutorial_3_done)
        {
            if (player.transform.position.x >= tutorial_3_position.position.x)
            {
                ActivateTutorial(3);
            }

        }
        else
        {
            DeactivateTutorial(3);
        }
    }

    void ActivateTutorial(int index)
    {
        if (index == 1)
        {
            MovementAttackTutorial.SetActive(true);
        }
        if (index == 2)
        {
            healthSpiritBarTutorial.SetActive(true);
        }
        if (index == 3)
        {
            ItemTutorial.SetActive(true);
        }
        Time.timeScale = 0;

    }
    public void DeactivateTutorial(int index)
    {
        if (index == 1)
        {
            MovementAttackTutorial.SetActive(false);
            tutorial_1_done = true;
        }
        if (index == 2)
        {
            healthSpiritBarTutorial.SetActive(false);
            tutorial_2_done = true;
        }
        if (index == 3)
        {
            ItemTutorial.SetActive(false);
            tutorial_3_done = true;
        }

    }
    public void DisableTutorial1()
    {
        MovementAttackTutorial.SetActive(false);
    }

    public void DisableTutorial2()
    {
        healthSpiritBarTutorial.SetActive(false);
    }

    public void DisableTutorial3()
    {
        ItemTutorial.SetActive(false);
    }
}
