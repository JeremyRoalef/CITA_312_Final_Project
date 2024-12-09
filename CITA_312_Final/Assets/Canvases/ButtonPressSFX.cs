using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class will be responsible for playing the button sound effect. This class is attached to canvas game objects with buttons
 */
public class ButtonPressSFX : MonoBehaviour
{
    //Serialized fields
    [SerializeField]
    [Tooltip("The button SFX")]
    AudioClip buttonSFX;

    //Cashe references
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log($"No audio source on {gameObject.name}. Creating audio source...");
            audioSource =  gameObject.AddComponent<AudioSource>();
            audioSource.clip = buttonSFX;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        if (audioSource.clip == null)
        {
            Debug.Log($"No audio clip. Drag the audio clip in {gameObject.name} into this script or the audio source component. Removing script...");
            Destroy(this);
        }
    }

    public void PlayButtonPressSFX()
    {
        audioSource.Play();
    }
}
