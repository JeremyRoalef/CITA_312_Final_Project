using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectCanvas : MonoBehaviour
{
    public void OnButtonNextLevelClick()
    {
        Debug.Log("Showing next level...");
    }
    public void OnButtonPreviousLevelClick()
    {
        Debug.Log("Showing previous level...");
    }
    public void OnButtonLevelSelectClick()
    {
        Debug.Log("Loading Level...");

        //Load test scene for now
        SceneManager.LoadScene("TestScene");
    }
}
