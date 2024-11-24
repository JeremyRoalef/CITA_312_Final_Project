using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This scriptable object will store the player abilities the level will allow
 */

[CreateAssetMenu(fileName = "LevelAbilities", menuName = "ScriptableObject/LevelAbilities")]
public class LevelAbilitiesSO : ScriptableObject
{
    [Tooltip("Add the allowed player abilities to this array")]
    public PlayerAbilitySO[] abilities;
}
