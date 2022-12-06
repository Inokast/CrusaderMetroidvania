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

/* Desc.
 * This class manages spawning the player in a new level
 * 
 * How to use:
 * Add script to empty game object named "PlayerSpawnManager"
 * Put in empty game objects where you want the player to spawn and give them the "PlayerSpawner" tag
 * Rename any "PlayerSpawner" objects to the correlating previous level with "From" added to the beggining. Ex. "FromCaveRoomOne"
 * 
 */ 

public class PlayerSpawnBehavior : MonoBehaviour
{
    GameObject player;

    //This Script is called by the GameManager script to place the player at a point relative to the previous level.

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlacePlayer(string lastScene)
    {
        GameObject spawnPoint;
        string tempName = "From" + lastScene;
        try
        {
            //Attempt to find the correlating spawn point and set the spawnPoint GO equal to that point
            Debug.Log("Looking for: " + tempName);
            spawnPoint = GameObject.FindGameObjectWithTag(tempName);
            Debug.Log(spawnPoint.name);
            player.transform.position = spawnPoint.transform.position;
        }
        catch
        {
            //If a proper spawn point can't be found, try to spawn them at a default location
            try
            {
                spawnPoint = GameObject.Find("DefaultSpawn");
                player.transform.position = spawnPoint.transform.position;
            }
            catch
            {
                //If no spawnpoint can be found give an error message
                Debug.Log("No suitable spawn points located. Please create a \"DefaultSpawn\" object and a \"From" + lastScene + "\" object");
            }
            //NoSpawnpointFound
            Debug.Log("No proper spawn points located. Please create a \"From" + lastScene + "\" object");
        }
    }
}
