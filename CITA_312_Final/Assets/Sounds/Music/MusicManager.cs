using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the music that will be playing in the start screen and level select scene")]
    AudioClip defaultMusic;

    [SerializeField]
    [Tooltip("This is the music that will be playing in Level 1")]
    AudioClip level1Music;

    [SerializeField]
    [Tooltip("This is the music that will be playing in Level 2")]
    AudioClip level2Music;

    //Singleton
    public static GameObject instance;
    static AudioSource audioSource;

    public enum MusicType
    {
        defaultMusic,
        level1Music,
        level2Music
    }

    MusicType music;
    public MusicType Music
    {
        set
        {
            changeMusicClip = true;
            music = value;
        }
    }

    bool changeMusicClip = false;

    void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }

        audioSource = instance.GetComponent<AudioSource>();
    }

    void Start()
    {
        //Start by playing the default music
        Music = MusicType.defaultMusic;
    }

    void Update()
    {
        if (changeMusicClip)
        {
            switch (music)
            {
                case MusicType.defaultMusic:
                    ChangeMusicClip(defaultMusic);
                    break;
                case MusicType.level1Music:
                    ChangeMusicClip(level1Music);
                    break;
                case MusicType.level2Music:
                    ChangeMusicClip(level2Music);
                    break;
            }
        }
    }

    void ChangeMusicClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
        changeMusicClip = false;
    }
}
