using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assignment/Lab/Project: Metroidvania
//Name: Talyn Epting

public class Health
{
    int health;
    int maxHealth;


    public int _Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health < 0)
            {
                health = 0;
            }
        }
    }

    public void Damagehealth(float damageAmt)
    {
        _Health -= (int)damageAmt;
    }

    public void RestoreHealth(float restoreAmt)
    {
        _Health += (int)restoreAmt;
    }
}
