using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    enum PushBehavior
    {
        Up,
        Away
    }

    [SerializeField]
    [Min(0)]
    float fltForceAmount = 100f;

    [SerializeField]
    PushBehavior pushBehavior;

    [SerializeField]
    float fltPlayerLockoutDuration = 1f;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log($"No Audio Source Found In {gameObject.name}");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PushPlayer(other);
            PlayPushSound();
        }
    }
    private void PushPlayer(Collision other)
    {
        other.gameObject.GetComponent<PlayerMover>().LockMovement(fltPlayerLockoutDuration);

        switch (pushBehavior)
        {
            case PushBehavior.Up:
                //Apply force relative to the object's upward vector
                Vector3 upVector = transform.up;
                other.rigidbody.AddForce(upVector * fltForceAmount);
                break;
            case PushBehavior.Away:
                //Apply force relative to the object's center & other's position
                Vector3 centerPos = transform.position;
                Vector3 forceDir = (other.transform.position - centerPos).normalized;
                other.rigidbody.AddForce(forceDir * fltForceAmount);
                break;
        }
    }
    void PlayPushSound()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
