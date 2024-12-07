using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectCanvas : MonoBehaviour
{
    //Integer will store the index of the levels. Level 1 is index 0 and so on
    int intLevelIndex = 0;

    [SerializeField]
    [Tooltip("Drag the levels in ascending order here")]
    SceneAsset[] levelScenes;

    [SerializeField]
    [Tooltip("Drag the level abilities in ascending order here. (level 1 goes with top level abilities and so on)")]
    LevelAbilitiesSO[] levelAbilities;

    [SerializeField]
    [Tooltip("This text field is for the button in the middle of the canvas")]
    TextMeshProUGUI levelNameText;

    private void Start()
    {
        DisplayLevel(intLevelIndex);
    }

    public void OnButtonNextLevelClick()
    {
        if (intLevelIndex < levelScenes.Length - 1)
        {
            Debug.Log("Showing next level...");
            intLevelIndex++;
            DisplayLevel(intLevelIndex);
        }
        else
        {
            Debug.Log("Already at highest level");
        }
    }
    public void OnButtonPreviousLevelClick()
    {
        if (intLevelIndex > 0)
        {
            Debug.Log("Showing previous level");
            intLevelIndex--;
            DisplayLevel(intLevelIndex);
        }
        else
        {
            Debug.Log("Already at lowest level");
        }
    }
    public void OnButtonLevelSelectClick()
    {
        LoadLevel(intLevelIndex);
        DisplayLevelAbilities();
        ChangeMusicClip();
    }

    private void ChangeMusicClip()
    {
        switch (intLevelIndex)
        {
            //level 1
            case 0:
                //What a line of code
                MusicManager.instance.GetComponent<MusicManager>().Music = MusicManager.MusicType.level1Music;
                break;
            //Level 2
            case 1:
                MusicManager.instance.GetComponent<MusicManager>().Music = MusicManager.MusicType.level2Music;
                break;
            //Level music not added
            default:
                MusicManager.instance.GetComponent<MusicManager>().Music = MusicManager.MusicType.defaultMusic;
                break;
        }
    }

    void DisplayLevel(int levelIndex)
    {
        levelNameText.text = $"Level {levelIndex + 1}";
    }

    void DisplayLevelAbilities()
    {

    }

    void LoadLevel(int levelIndex)
    {
        //I found a way to consistently load scenes by their name, even if the name changes!
        SceneManager.LoadScene(levelScenes[levelIndex].name);
    }
    public void OnButtonBackClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}


