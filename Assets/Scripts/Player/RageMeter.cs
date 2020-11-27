using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageMeter : MonoBehaviour
{
    //-------------Get the slider component
    public Slider slider;
    //-----------------------Get player current stat
    private PlayerStat playerStat;
    void Awake()
    {
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = playerStat.maxRage;
        slider.value = playerStat.Rage;
    }
    void Update()
    {


        if (slider.value != playerStat.Rage)
        {
            slider.value = playerStat.Rage;
        }

        if (slider.maxValue != playerStat.maxRage)
        {
            slider.maxValue = playerStat.maxRage;

        }
    }
}
