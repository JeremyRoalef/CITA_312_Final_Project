using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * This script will hold the time the player spends in a level. This script will also talk to the timer canvas
 * to display the time the player has been in the level.
 */
public class Timer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The timer text in the scene")]
    TextMeshProUGUI timerText;

    float elapsedTime;
    public float ElapsedTime
    {
        get
        {
            return elapsedTime;
        }
        set
        {
            elapsedTime = value;
            timerText.text = elapsedTime.ToString("F2");
        }
    }
    void Start()
    {
        ElapsedTime = 0;
    }
    void Update()
    {
        ElapsedTime += Time.deltaTime;
    }
}
