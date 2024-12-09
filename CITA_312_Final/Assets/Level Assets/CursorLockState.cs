using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is not attached to any game object.
 * 
 * This class will be responsible for locking and unlocking the player's cursor
 */

public class CursorLockState : MonoBehaviour
{
    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
