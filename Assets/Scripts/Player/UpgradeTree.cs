using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTree : MonoBehaviour
{
    public GameObject upgrade1;
    public GameObject upgrade2;
    public GameObject warningSign1;
    public GameObject upgrade3;
    public GameObject upgrade4;
    public GameObject warningSign2;
    public Quest quests;

    void Update()
    {
        if (quests.Quest_1_finish)
        {
            warningSign1.SetActive(false);
            upgrade1.SetActive(true);
            upgrade2.SetActive(true);
        }
        else
        {
            warningSign1.SetActive(true);
            upgrade1.SetActive(false);
            upgrade2.SetActive(false);
        }
        if (quests.Quest_15_finish)
        {
            warningSign2.SetActive(false);
            upgrade3.SetActive(true);
            upgrade4.SetActive(true);
        }
        else
        {
            warningSign2.SetActive(true);
            upgrade3.SetActive(false);
            upgrade4.SetActive(false);
        }
    }
}
