using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attacehd to the controls canvas.
 * 
 * This script will be responsible for changing the player's controls
 */
public class ControlsCanvas : MonoBehaviour
{
    //Singleton canvas
    public static GameObject instance;

    private void Awake()
    {
        //Singleton Pattern
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Hide the canvas
        gameObject.SetActive(false);
    }

    public void OnButtonBackClick()
    {
        BackTrackCanvasStack.ReturnToPreviousCanvas(gameObject);
    }
}
