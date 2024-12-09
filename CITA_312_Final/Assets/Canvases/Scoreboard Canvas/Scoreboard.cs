using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Drag the level scriptable object into this field")]
    LevelSO levelSO;

    TimerCanvas timerCanvas;

    [SerializeField]
    [Tooltip("Enter the score text game object here")]
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        timerCanvas = FindObjectOfType<TimerCanvas>();
    }

    private void OnEnable()
    {
        //Get the timer canvas in scene
        CalculateAndDisplayScore();
        CursorLockState.UnlockCursor();
    }

    public void OnButtonReturnToLevelSelectClick()
    {
        MusicManager.instance.GetComponent<MusicManager>().Music = MusicManager.MusicType.defaultMusic; //This code is really getting out of hand
        SceneManager.LoadScene("LevelSelect");
    }

    void CalculateAndDisplayScore()
    {
        float score = levelSO.CalculateScore(timerCanvas.ElapsedTime);
        scoreText.text = $"SCORE: {score} ({levelSO.CalculateScorePercentage(timerCanvas.ElapsedTime).ToString("F2")}% of max)";

        //Update total player score here
    }
}
