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
    bool collidedWithPlayer = false;

    private void OnCollisionEnter(Collision other)
    {
        if (collidedWithPlayer)
        {
            return;
        }

        //IM TRIGGERED (Let me speak to your manager)
        collidedWithPlayer = true;

        if (other.gameObject.CompareTag("Player"))
        {
            InitiateDropSequence();
        }
    }

    void InitiateDropSequence()
    {
        Invoke("EnableGravity", fltGravityActivationDelay);
        Invoke("DestroyDropper", fltDestroyDelay);
    }

    void EnableGravity()
    {
        transform.AddComponent<Rigidbody>();
    }
    void DestroyDropper()
    {
        Destroy(gameObject);
    }
}
