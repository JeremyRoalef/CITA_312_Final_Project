using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsCanvas : MonoBehaviour
{
    //Singleton Canvas
    public static GameObject instance;

    private void Awake()
    {
        //Singleton Pattern
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        //Hide the canvas
        gameObject.SetActive(false);
    }

    //Method for back button
    public void OnButtonBackClick()
    {
        BackTrackCanvasStack.ReturnToPreviousCanvas(gameObject);
    }

    //Method for audio button
    public void OnButtonAudioClick()
    {
        //Add current canvas to the stack, hide the current canvas, show the audio canvas
        BackTrackCanvasStack.AddCanvasToStack(gameObject);
        gameObject.SetActive(false);
        AudioCanvas.instance.SetActive(true);

    }

    //Method for controls button
    public void OnButtonControlsClick()
    {
        //Add current canvas to the stack, hide the current canvas, show the controls canvas
        BackTrackCanvasStack.AddCanvasToStack(gameObject);
        gameObject.SetActive(false);
        ControlsCanvas.instance.SetActive(true);
    }
}