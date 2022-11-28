using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//////////////////////////////////////////////
//Assignment/Lab/Project: Metroidvania
//Name: Daniel Sanchez, Andrew Krieps, Talyn Epting
//Section: 2019SP.SGD.285.2144
//Instructor: Aurore Locklear
//Date: 11/7/2022
/////////////////////////////////////////////

public class StatBars : MonoBehaviour
{
    public static StatBars sb;

    [SerializeField] Slider hpSlider;
    [SerializeField] Slider manaSlider;

    public void SetMaxHealth(int health)
    {
        hpSlider.maxValue = health;
    }
    public void SetMaxMana(int mana)
    {
        manaSlider.maxValue = mana;
    }

    public void UpdateHealth(int health)
    {
        hpSlider.value = health;
    }
    public void UpdateMana(int mana)
    {
        manaSlider.value = mana;
    }
}
