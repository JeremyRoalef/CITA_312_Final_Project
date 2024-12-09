using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This scriptable object class will be used to store information about player abilities for ease of use.
 * In this scriptable object, the name, desciption, and image of the ability will be stored.
 * This class will not store the player's ability functionalities
 */
[CreateAssetMenu(fileName = "PlayerAbility", menuName = "ScriptableObject/PlayerAbility")]
public class PlayerAbilitySO : ScriptableObject
{
    [Header("Desctiptives")]

    [Tooltip("The name of the ability")]
    public string abilityName;

    [Tooltip("Describe this ability")]
    [TextArea(2,10)]
    public string description;

    [Tooltip("Drag the ability's image here")]
    public Sprite abilityImage;


    [Header("Functionalities")]

    [Min(1)]
    [Tooltip("How much will this ability cost the player?")]
    public int cost = 1;

    [Tooltip("Drag in the required abilities here")]
    public PlayerAbilitySO[] requirements;

    [Tooltip("Drag the restricted abilities here")]
    public PlayerAbilitySO[] restrictions;

    //Setting default sprite image when the scriptable object is made. Credit for idea: ChatGPT
    //The OnEnable is called after naming the scriptable object.
    void OnEnable()
    {
        if (abilityImage == null)
        {
            abilityImage = Resources.Load<Sprite>("Player Ability Sprites/DefaultSprite");
        }
    }
}
