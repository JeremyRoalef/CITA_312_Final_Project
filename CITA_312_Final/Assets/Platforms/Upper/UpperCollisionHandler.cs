using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperCollisionHandler : MonoBehaviour
{
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
