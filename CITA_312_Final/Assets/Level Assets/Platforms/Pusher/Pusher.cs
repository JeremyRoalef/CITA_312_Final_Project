using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * This script is attached to the pusher prefab.
 * 
 * This script will be responsible for pushing object based on the push behavior
 */
public class Pusher : MonoBehaviour
{
    //Enumerator for push behavior
    enum PushBehavior
    {
        Up,
        Away
    }

    //Serialized fields
    [SerializeField]
    [Min(0)]
    [Tooltip("The amound of force the pusher will apply to the other game object.")]
    float fltForceAmount = 100f;

    [SerializeField]
    [Tooltip("How should this pusher push object?")]
    PushBehavior pushBehavior;

    [SerializeField]
    [Tooltip("The time the player's movement will be disabled")]
    float fltPlayerLockoutDuration = 1f;

    //Cashe references.
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
        //If colliding with player, push the player
        if (other.gameObject.CompareTag("Player"))
        {
            PushPlayer(other);
            PlayPushSound();
        }
    }

    private void PushPlayer(Collision other)
    {
        //lock player's movement
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
