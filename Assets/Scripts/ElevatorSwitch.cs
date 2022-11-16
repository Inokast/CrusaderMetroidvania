using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////
//Assignment/Lab/Project: Metroidvania
//Name: Daniel Sanchez, Andrew Krieps, Talyn Epting
//Section: 2019SP.SGD.285.2144
//Instructor: Aurore Locklear
//Date: 11/7/2022
/////////////////////////////////////////////

public class ElevatorSwitch : MonoBehaviour
{
    [SerializeField] private GameObject targetElevator;
    private bool active = true;
    [SerializeField] private string switchPoint = null;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Staff" || collision.gameObject.tag == "Spell")
        {
            ContactElevator();
        }
    }

    public void ContactElevator()
    {
        if(switchPoint == "A")
        {
            targetElevator.GetComponent<ElevatorBehavior>().RecieveContact("A");
        }
        if (switchPoint == "B")
        {
            targetElevator.GetComponent<ElevatorBehavior>().RecieveContact("B");
        }
    }
}
