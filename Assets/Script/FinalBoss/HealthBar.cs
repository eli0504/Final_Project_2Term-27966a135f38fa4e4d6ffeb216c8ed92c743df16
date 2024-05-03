using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health; //make sure that our slider starts at the max amount of health.
    }

   public void SetHealth(int health)
    {
        slider.value = health;
    }
}
