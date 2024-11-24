using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] InputAction pauseAction;
    [SerializeField] GameObject pauseCanvas;

    public static bool gameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;   
    }

    void PauseGame()
    {
        pauseCanvas.SetActive(true);

        //The time scale is responsible for controlling how time passes in the game. Setting it to 0
        //means that time does not move
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void OnButtonResumeClick()
    {
        ResumeGame();
    }

    public void OnButtonQuitClick()
    {
        ResumeGame();

        //Logic when quitting scene
        switch (SceneManager.GetActiveScene().name)
        {
            case "LevelSelect":
                SceneManager.LoadScene("StartScene");
                break;
            default:
                SceneManager.LoadScene("LevelSelect");
                break;
        }
    }

    public void OnButtonSettingsClick()
    {
        SettingsCanvas.instance.SetActive(true);
        BackTrackCanvasStack.AddCanvasToStack(gameObject);
        gameObject.SetActive(false);
    }
}
