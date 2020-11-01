using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public void SetMaxHealth(float health)
    {
        //Set the max value for health
        slider.maxValue = health;
        //Set the current amount of health
        slider.value = health;

    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

}
