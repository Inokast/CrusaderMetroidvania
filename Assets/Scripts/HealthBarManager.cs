using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//////////////////////////////////////////////
//Assignment/Lab/Project: Metroidvania
//Name: Daniel Sanchez, Andrew Krieps, Talyn Epting
//Section: 2019SP.SGD.285.2144
//Instructor: Aurore Locklear
//Date: 11/7/2022
/////////////////////////////////////////////

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager hpbm;

    [SerializeField] Slider hpSlider;

    public void SetMaxHealth(int health)
    {
        hpSlider.maxValue = health;
    }
    public void UpdateHealth(int health)
    {
        hpSlider.value = health;
    }
}
