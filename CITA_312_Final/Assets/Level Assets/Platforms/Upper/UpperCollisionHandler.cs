using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the 'top' game object in the upper prefab.
 * 
 * This script is responsible for controlling the upper collisions and telling the upper prefab what to do and when.
 */
public class UpperCollisionHandler : MonoBehaviour
{
    //Cashe references
    Upper upper;

    private void Awake()
    {
        upper = GetComponentInParent<Upper>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            upper.CallMoveToTargetCoroutine();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            upper.CallMoveToStartCoroutine();
        }
    }
}
