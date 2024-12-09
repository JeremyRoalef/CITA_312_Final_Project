using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script is attached to the start screen canvas in the starting scene.
 * 
 * This script will be responsible for handling button pressing.
 */
public class StartScreenCanvas : MonoBehaviour
{
    public void OnButtonStartClick()
    {
        Debug.Log("Starting the game");
        //Load the levelSO select scene
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnButtonTutorialClick()
    {
        Debug.Log("Opeining tutorial scene");
        SceneManager.LoadScene("TutorialScene");

    }

    public void OnButtonSettingsClick()
    {
        //Open settings canvas, hide current canvas, add current canvas to the backtrack stack
        SettingsCanvas.instance.SetActive(true);
        BackTrackCanvasStack.AddCanvasToStack(gameObject);
        gameObject.SetActive(false);
    }

    public void OnButtonQuitClick()
    {
        Debug.Log("Quitting the application");
    }
}
