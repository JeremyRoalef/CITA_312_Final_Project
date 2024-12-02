using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    float fltGravityActivationDelay = 0.2f;

    [SerializeField]
    [Min(0)]
    float fltDestroyDelay = 1f;

    public void InitiateDropSequence()
    {
        Invoke("EnableGravity", fltGravityActivationDelay);
        Invoke("DestroyDropper", fltDestroyDelay);
    }

    //Used in drop sequence
    void EnableGravity()
    {
        transform.AddComponent<Rigidbody>();
    }
    //Used in drop sequence
    void DestroyDropper()
    {
        Destroy(gameObject);
    }
}
