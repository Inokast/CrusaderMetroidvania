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
        if(sfx == null)
        {
            sfx = this;
        }
    }

    void Start()
    {
        
    }

    public void PlaySound(int s)
    {
        //method for playing various sfx in games, to be filled out later- T.E.
        
    }
}
