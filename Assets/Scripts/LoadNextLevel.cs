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

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] string nextLevelName = "";
    [SerializeField] GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            try
            {
                gm.SetPreviousScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene(nextLevelName);
            }
            catch
            {
                Debug.Log(nextLevelName + " is not an available level, please make sure the scene name is correct.");
            }
        }
    }
}
