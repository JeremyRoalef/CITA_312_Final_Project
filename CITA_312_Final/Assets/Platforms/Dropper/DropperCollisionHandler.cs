using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperCollisionHandler : MonoBehaviour
{
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
