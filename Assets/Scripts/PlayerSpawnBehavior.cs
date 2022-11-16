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

public class PlayerSpawnBehavior : MonoBehaviour
{
    [SerializeField] private GameObject[] PlayerSpawns;
    [SerializeField] GameManager gm;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlacePlayer(string lastScene)
    {
        GameObject spawnPoint;
        try
        {
            spawnPoint = GameObject.Find("From" + lastScene);
        }
        catch
        {
            spawnPoint = GameObject.Find("DefaultSpawn");
            //NoSpawnpointFound
        }

        Instantiate(player, spawnPoint.transform.position, Quaternion.identity);
    }
}
