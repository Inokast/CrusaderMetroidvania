using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assignment/Lab/Project: Metroidvania
//Name: Talyn Epting

public class SFX : MonoBehaviour
{
    public static SFX sfx;

    [SerializeField] AudioSource[] sounds;


    void Awake()
    {
        if (sfx == null)
        {
            sfx = this;
        }
    }

    public void PlaySound(int s)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (s == i)
            {
                sounds[i].Play();
            }
        }
    }
}
