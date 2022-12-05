using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider vol;

    void Start()
    {
        if (!PlayerPrefs.HasKey("sound")) { PlayerPrefs.SetFloat("sound", 1); Load(); }
        else { Load(); }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = vol.value;
        Save();
    }

    void Save()
    {
        PlayerPrefs.SetFloat("sound", vol.value);
    }

    void Load()
    {
        vol.value = PlayerPrefs.GetFloat("sound");
    }
}
