using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script is attached to the scoreboard canvas. This script will be responsible for displaying the player's score at the
 * end of the level and returning to the level select scene.
 */
public class ScoreboardCanvas : MonoBehaviour
{
    //Serialized fields
    [SerializeField]
    [Tooltip("Drag the level scriptable object into this field")]
    LevelSO levelSO;

    [SerializeField]
    [Tooltip("Enter the score text game object here")]
    TextMeshProUGUI scoreText;

    //Cashe References
    TimerCanvas timerCanvas;

    private void Awake()
    {
        timerCanvas = FindObjectOfType<TimerCanvas>();
    }

    private void OnEnable()
    {
        //Get the timer canvas in scene
        CalculateAndDisplayScore();

        //Allow the player to move their cursor
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
