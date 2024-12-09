using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/*
 * This script is attached to the main camera in the camera prefab
 * 
 * This script is responsible for controlling camera behavior.
 */
public class CameraManager : MonoBehaviour
{
    void Update()
    {
        //This stops the camera from spinning while the game is paused
        if (PauseCanvas.gameIsPaused)
        {
            GetComponent<CinemachineBrain>().enabled = false;
        }
        else
        {
            GetComponent<CinemachineBrain>().enabled = true;
        }
    }
}
