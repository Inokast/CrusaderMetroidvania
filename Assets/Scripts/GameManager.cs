using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//////////////////////////////////////////////
//Assignment/Lab/Project: Metroidvania
//Name: Daniel Sanchez, Andrew Krieps, Talyn Epting
//Section: 2019SP.SGD.285.2144
//Instructor: Aurore Locklear
//Date: 11/7/2022
/////////////////////////////////////////////

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [Header("UI Values")]       //I figure the game manager can just directly handle UI this time?- T.E.
    TextMeshProUGUI scoreText;
    public GameObject optionsPanel, quitPanel;

    [Header("In-game Values")]
    int score;


    void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //buttons here
    #region buttons
    public void OnPlayClick()
    {
        SceneManager.LoadScene("TestLevel");
        Debug.Log("started game");
    }

    public void OnQuitClick()
    {
        quitPanel?.SetActive(true);
    }

    public void YesQuit()
    {
        Application.Quit();
        Debug.Log("quit game");
    }

    public void NoQuit()
    {
        quitPanel?.SetActive(false);
        Debug.Log("did not quit game");
    }

    public void OnOptionsClick()
    {
        optionsPanel?.SetActive(true);
    }

    public void OnCloseClick()
    {
        optionsPanel?.SetActive(false);
    }
    #endregion
}
