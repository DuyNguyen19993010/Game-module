using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rage : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public void SetMaxRage(float rage)
    {
        //Set the max value for health
        slider.maxValue = rage;
        //Set the current amount of health
        slider.value = rage;

    }

    public void SetRage(float rage)
    {
        slider.value = rage;
    }

}
