using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script is attached to the win location prefab.
 * 
 * This script is responsible for the behavior involving the player reaching the end of the level.
 */

//This script requires an audio source
[RequireComponent(typeof(AudioSource))]
public class WinLocation : MonoBehaviour
{
    //Serialized fields
    [SerializeField]
    [Tooltip("Drag the scoreboard canvas here")]
    GameObject scoreboard;

    //Cashe references
    AudioSource audioSource;

    //Attributes
    bool playerHasWon = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playerHasWon)
        {
            Debug.Log("You beat the level");
            StartWinSequence();
        }
    }

    void StartWinSequence()
    {
        playerHasWon = true;
        PlayWinSound();
        DisplayScoreboard();
    }

    void DisplayScoreboard()
    {
        scoreboard.SetActive(true);
    }

    void PlayWinSound()
    {
        audioSource.Play();
    }
}
