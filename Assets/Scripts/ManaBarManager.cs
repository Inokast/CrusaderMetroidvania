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

public class ManaBarManager : MonoBehaviour
{
    public static ManaBarManager mabm;

    [SerializeField] Slider manaSlider;

    public void SetMaxMana(int mana)
    {
        manaSlider.maxValue = mana;
    }
    public void UpdateMana(int mana)
    {
        manaSlider.value = mana;
    }
}
