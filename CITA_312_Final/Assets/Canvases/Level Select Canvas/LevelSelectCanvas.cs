using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script is attached to the level select canvas and is responsible for selecting and starting levels.
 */
public class LevelSelectCanvas : MonoBehaviour
{
    //Serialized fields
    [SerializeField]
    [Tooltip("Drag the levels in ascending order here")]
    LevelSO[] levels;

    [SerializeField]
    [Tooltip("Drag the level abilities in ascending order here. (level 1 goes with top level abilities and so on)")]
    LevelSO[] levelAbilities;

    [SerializeField]
    [Tooltip("This text field is for the button in the middle of the canvas")]
    TextMeshProUGUI levelNameText;

    //Attributes
    int intLevelIndex = 0; //Integer will store the index of the levels. Level 1 is index 0 and so on

    private void Start()
    {
        DisplayLevel(intLevelIndex);
    }

    public void OnButtonNextLevelClick()
    {
        if (intLevelIndex < levels.Length - 1)
        {
            //Debug.Log("Showing next level...");
            intLevelIndex++;
            DisplayLevel(intLevelIndex);
        }
    }
    public void OnButtonPreviousLevelClick()
    {
        if (intLevelIndex > 0)
        {
            //Debug.Log("Showing previous level");
            intLevelIndex--;
            DisplayLevel(intLevelIndex);
        }
    }
    public void OnButtonLevelSelectClick()
    {
        LoadLevel(intLevelIndex);
        //DisplayLevelAbilities();
        ChangeMusicClip();
        CursorLockState.LockCursor();
    }

    private void ChangeMusicClip()
    {
        switch (intLevelIndex)
        {
            //Level 1
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
        SceneManager.LoadScene(levels[levelIndex].levelScene);
    }
    public void OnButtonBackClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}


