using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenCanvas : MonoBehaviour
{
    //Method for start button
    public void OnButtonStartClick()
    {
        Debug.Log("Starting the game");
        //Load the level select scene
        SceneManager.LoadScene("LevelSelect");
    }

    //Method for tutorial button
    public void OnButtonTutorialClick()
    {
        Debug.Log("Opeining tutorial scene");
        SceneManager.LoadScene("TutorialScene");

    }

    //Method for settings button
    public void OnButtonSettingsClick()
    {
        //Open settings canvas, hide current canvas, add current canvas to the backtrack stack
        SettingsCanvas.instance.SetActive(true);
        BackTrackCanvasStack.AddCanvasToStack(gameObject);
        gameObject.SetActive(false);
    }

    //Method for quit button
    public void OnButtonQuitClick()
    {
        Debug.Log("Quitting the application");
    }
}
