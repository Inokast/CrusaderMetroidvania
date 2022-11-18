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
    public GameObject optionsPanel, helpPanel, quitPanel;

    [Header("In-game Values")]
    int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    string previousScene = null;
    private GameObject playerSpawner;


    void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
    }

    void Start()
    {
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner");
        CallPlayerSpawner(previousScene);
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

    //for the help event trigger in options panel
    public void OnHelpMouseOver()
    {
        helpPanel?.SetActive(true);
    }

    public void OnHelpMouseAway()
    {
        helpPanel?.SetActive(false);
    }
    #endregion

    public void SetPreviousScene(string preScene)
    {
        previousScene = preScene;
    }

    private void CallPlayerSpawner(string lastScene)
    {
        playerSpawner.GetComponent<PlayerSpawnBehavior>().PlacePlayer(lastScene);
    }
}
