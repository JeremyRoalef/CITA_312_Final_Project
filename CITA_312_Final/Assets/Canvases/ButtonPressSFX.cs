using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressSFX : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log($"No audio source on {gameObject.name}. Disabling script...");

        }
    }

    public void PlayButtonPressSFX()
    {
        audioSource.Play();
    }
    public void StopButtonPressSFX()
    {
        audioSource.Stop();
    }
}
