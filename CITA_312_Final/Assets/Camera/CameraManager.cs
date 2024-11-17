using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Do not ignore time scale
        GetComponent<CinemachineBrain>().m_IgnoreTimeScale = false;
    }

    private void Update()
    {
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
