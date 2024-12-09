using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the audio canvas. This script is responsible for controlling the in-game audio settings.
 */
public class AudioCanvas : MonoBehaviour
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
