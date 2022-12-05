using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//////////////////////////////////////////////
//Assignment/Lab/Project: Metroidvania
//Name: Daniel Sanchez, Andrew Krieps, Talyn Epting
//Section: 2022SP.SGD.285.2144
//Instructor: Aurore Locklear
//Date: 11/7/2022
/////////////////////////////////////////////

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [Header("UI Values")]       //I figure the game manager can just directly handle UI this time?- T.E.
    public GameObject optionsPanel, helpPanel, pausePanel, quitPanel;
    public GameObject dialogueDisplay;   //dialogueDisplay is meant for the trigger which the player encounters to fire off dialogue snippits
    public TextMeshProUGUI[] alert;    //alert is the space for actual alert strings (scriptable objects?)

    [Header("In-game Values")]
    public string previousScene = null;
    private PlayerSpawnBehavior playerSpawner;
    [SerializeField] bool paused;
    

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    void Start()
    {
        previousScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        Pause();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
            }
        }
    }

    //buttons here
    #region buttons
    public void OnPlayClick()
    {

        StartCoroutine(StartGame());
        Debug.Log("started game");
    }

    public void OnQuitClick()
    {
        quitPanel?.SetActive(true);
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
        Debug.Log("went to main menu");
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

    //Set Previous Scene is called by the LoadNextLevel class to set the scene that's being exited to keep the name
    public void SetPreviousScene(string preScene)
    {
        previousScene = preScene;
    }

    public void CallPlayerSpawner(string lastScene)
    {
        //Tells the PlayerSpawnManager to spawn the player
        playerSpawner = FindObjectOfType<PlayerSpawnBehavior>();
        playerSpawner.PlacePlayer(lastScene);
    }

    IEnumerator StartGame() 
    {
        LevelTransitionScreen transition = FindObjectOfType<LevelTransitionScreen>();

        transition.StartTransition();

        yield return new WaitForSeconds(transition.transitionTime);

        SceneManager.LoadScene("CaveRoomTwo");
    }
}
