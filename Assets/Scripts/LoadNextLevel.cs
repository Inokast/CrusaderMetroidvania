using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//////////////////////////////////////////////
//Assignment/Lab/Project: Metroidvania
//Name: Daniel Sanchez, Andrew Krieps, Talyn Epting
//Section: 2019SP.SGD.285.2144
//Instructor: Aurore Locklear
//Date: 11/7/2022
/////////////////////////////////////////////

/* How to Use
 * 
 * Place this script on an empty object with a 2D box collider that covers the entrance/exit.
 * In the object inspector type the name of the level in the "Next Level Name" area
 * 
 */

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] string nextLevelName = "";

    //This script tells the gamemanager which level the player is leaving and to load the next

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Is the collision the player
        if (collision.gameObject.CompareTag("Player"))
        {
            //Try/Catch to prevent crash
            try
            {
                //Tell GameManager the name of the level being exited
                gm.SetPreviousScene(SceneManager.GetActiveScene().name);
                //Loads the next scene
                SceneManager.LoadScene(nextLevelName);
            }
            catch
            {
                Debug.Log(nextLevelName + " is not an available level, please make sure the scene name is correct.");
            }
        }
    }
}
