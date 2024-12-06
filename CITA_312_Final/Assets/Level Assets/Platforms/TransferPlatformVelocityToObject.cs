using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This scipt will be attached to objects that move and need to transfer their velocity to any object that is
 * touching it.
 */

public class TransferPlatformVelocityToObject : MonoBehaviour
{
    Vector3 oldPos;

    private void OnCollisionEnter(Collision other)
    {
        Vector3 newPos = transform.position;
        //calculate the distance between the old & new pos. apply to other game object's position
        other.transform.position += (newPos - oldPos);
        oldPos = newPos;
    }

    private void OnCollisionStay(Collision other)
    {
        Vector3 newPos = transform.position;
        //calculate the distance between the old & new pos. apply to other game object's position
        other.transform.position += (newPos - oldPos);
        oldPos = newPos;
    }
}
