using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLocation : MonoBehaviour
{
    AudioSource audioSource;

    bool playerHasWon = false;

    [SerializeField]
    [Tooltip("Drag the scoreboard canvas here")]
    GameObject scoreboard;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log($"No audio source in {gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
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

    private void DisplayScoreboard()
    {
        scoreboard.SetActive(true);
    }

    private void PlayWinSound()
    {
        audioSource.Play();
    }
}
