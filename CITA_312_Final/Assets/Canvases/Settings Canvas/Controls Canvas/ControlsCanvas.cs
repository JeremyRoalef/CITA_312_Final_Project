using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Method for back button
    public void OnButtonBackClick()
    {
        BackTrackCanvasStack.ReturnToPreviousCanvas(gameObject);
    }
}
