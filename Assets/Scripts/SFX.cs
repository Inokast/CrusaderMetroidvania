using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assignment/Lab/Project: Metroidvania
//Name: Talyn Epting

public class SFX : MonoBehaviour
{
    public static SFX sfx;

    [SerializeField] AudioSource[] sounds;
    [SerializeField] AudioSource clipSFX;
    [SerializeField] AudioClip[] clips;


    void Awake()
    {
        if (sfx == null)
        {
            sfx = this;
        }
    }

    public void PlayGameSound(int s)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (s == i)
            {
                sounds[s].Play();
            }
        }
    }

    public void PlayUISound(int s)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (s == i)
            {
                clipSFX.PlayOneShot(clips[s]);
            }
        }
    }
}
