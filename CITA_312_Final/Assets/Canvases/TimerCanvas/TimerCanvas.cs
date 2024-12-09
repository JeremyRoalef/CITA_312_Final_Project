using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * This script is attached to the timer canvas.
 * 
 * This script will hold the time the player spends in a levelSO. This script will also talk to the timer canvas
 * to display the time the player has been in the levelSO.
 */
public class TimerCanvas : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The timer text in the scene")]
    TextMeshProUGUI timerText;

    //Attributes
    float elapsedTime;
    //Property for elapsed time
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
        //Initialize elapsed time
        ElapsedTime = 0;
    }
    void Update()
    {
        //Update elapsed time
        ElapsedTime += Time.deltaTime;
    }
}
