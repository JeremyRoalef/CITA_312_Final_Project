using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*
 * This class is responsible for pausing the game and showing the puase canvas. 
 * This script is attached to the pause canvas game object
 */
public class PauseCanvas : MonoBehaviour
{
    //Serialized fields
    [SerializeField]
    [Tooltip("The keyboard button that will be responsible for pausing the game")]
    InputAction pauseAction;

    [SerializeField]
    [Tooltip("Drag the panel game object attacehd to the canvas here")]
    GameObject panelCanvas;

    //Attributes
    public static bool gameIsPaused = false;

    void Start()
    {
        //Disable the canvas
        panelCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    void Update()
    {
        //If the pause button was not pressed, do not run.
        if (!pauseAction.WasPressedThisFrame()) { return; }


        if (gameIsPaused)
        {
            ResumeGame();
            //Prevent the player from moving their cursor
            CursorLockState.LockCursor();
        }
        else
        {
            PauseGame();
        }
    }

    void ResumeGame()
    {
        panelCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    void PauseGame()
    {
        panelCanvas.SetActive(true);

        //The time scale is responsible for controlling how time passes in the game. Setting it to 0
        //means that time does not move
        Time.timeScale = 0;
        gameIsPaused = true;

        //Allow the player to move their cursor
        CursorLockState.UnlockCursor();
    }

    public void OnButtonResumeClick()
    {
        ResumeGame();
        //Prevent the player from moving their cursor
        CursorLockState.LockCursor();
    }

    public void OnButtonQuitClick()
    {
        ResumeGame();

        //Change music to default music
        MusicManager.instance.GetComponent<MusicManager>().Music = MusicManager.MusicType.defaultMusic; //This line is getting out of hand
        
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnButtonSettingsClick()
    {
        SettingsCanvas.instance.SetActive(true);
        BackTrackCanvasStack.AddCanvasToStack(gameObject);
        gameObject.SetActive(false);
    }
}
