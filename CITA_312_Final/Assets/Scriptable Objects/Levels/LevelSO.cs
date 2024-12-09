using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This scriptable object will store the player abilities the levelSO will allow
 */

[CreateAssetMenu(fileName = "LevelAbilities", menuName = "ScriptableObject/LevelAbilities")]
public class LevelSO : ScriptableObject
{
    [Tooltip("Add the allowed player abilities to this array")]
    public PlayerAbilitySO[] abilities;

    [Tooltip("Drag the level's scene here")]
    public string levelScene = "Enter the Level Name Here";

    [Range(0f, 1000f)]
    [Tooltip("Enter the minimum score here")]
    public float minScore;

    [Range(0f,1000f)]
    [Tooltip("Enter the maximum score here")]
    public float maxScore;

    [Tooltip("Enter the quickest estimated time to complete the level here")]
    [Min(0)]
    public float quickestTimeToCompleteLevel;

    [Tooltip("Enter the slowest estimated time to complete the level here")]
    [Min(0)]
    public float slowestTimeToCompleteLevel;

    void OnValidate()
    {
        //check min & max score
        if (minScore > maxScore)
        {
            Debug.Log("Minimum score cannot be greater than maximum score");
            minScore = maxScore;
        }
        if (slowestTimeToCompleteLevel < quickestTimeToCompleteLevel)
        {
            Debug.Log("slowest time to complete level cannot exceed quickest time to complete level");
            slowestTimeToCompleteLevel = quickestTimeToCompleteLevel;
        }
    }

    public float CalculateScore(float levelCompletionTime)
    {
        float calculatedScore = 0f;
        //If time is more than slowest time, return min score. If below quickest time, return max score. Otherwise, score is equal to below
        //Score = ( (time - min time)/(max time - min time) ) * total possible score
        if (levelCompletionTime > slowestTimeToCompleteLevel)
        {
            calculatedScore = minScore;
        }
        else if (levelCompletionTime < quickestTimeToCompleteLevel)
        {
            calculatedScore = maxScore;
        }
        else
        {
            calculatedScore = maxScore * (quickestTimeToCompleteLevel/levelCompletionTime);
        }
        return calculatedScore;
    }

    public float CalculateScorePercentage(float levelCompletionTime)
    {
        float scorePercent = 0f;
        //If time is more than slowest time, return min score. If below quickest time, return max score. Otherwise, score is equal to below
        //Score = ( (time - min time)/(max time - min time) ) * total possible score
        if (levelCompletionTime > slowestTimeToCompleteLevel)
        {
            scorePercent = 0f;
        }
        else if (levelCompletionTime < quickestTimeToCompleteLevel)
        {
            scorePercent = 100f;
        }
        else
        {
            scorePercent = (quickestTimeToCompleteLevel / levelCompletionTime);
        }
        return scorePercent;
    }
}
