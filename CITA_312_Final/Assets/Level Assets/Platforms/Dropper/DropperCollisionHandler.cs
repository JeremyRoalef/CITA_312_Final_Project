using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the 'top' game object in the dropper prefab.
 * 
 * This script is responsible for handling collisions in the game and telling the dropper when to do things.
 */
public class DropperCollisionHandler : MonoBehaviour
{
    //Attributes
    bool collidedWithObject = false;

    private void OnCollisionEnter(Collision other)
    {
        if (collidedWithObject) { return; }

        //Tell the parent object to drop
        if (other.gameObject.CompareTag("Player"))
        {
            collidedWithObject = true;
            GetComponentInParent<Dropper>().InitiateDropSequence();
        }
    }
}
