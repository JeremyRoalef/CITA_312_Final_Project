using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the crusher prefab.
 * 
 * This script is responsible for crusher behavior.
 */
public class Crusher : MonoBehaviour
{
    //Serialized fields
    [SerializeField]
    [Tooltip("How far should the object move in the given direction?")]
    float fltDistanceToMove = 1f;

    [SerializeField]
    [Min(0)]
    [Tooltip("The speed the object should move at")]
    float objVelocity = 10f;

    //Attributes
    Vector3 velocityDir;
    Vector3 startPos;
    Vector3 endPos;

    bool isMoving = false;
    bool moveToEnd = true;

    private void Awake()
    {
        //Velocity will always be relative to the object's up direction.
        velocityDir = transform.up;

        //Initialize start & end position
        startPos = transform.position;
        endPos = transform.position + (velocityDir * fltDistanceToMove);
    }

    void Start()
    {
        //Move to end
        StartCoroutine(MoveToEndPosition());
    }

    void Update()
    {
        if (isMoving) { return; }

        if (moveToEnd)
        {
            StartCoroutine(MoveToEndPosition());
        }
        else
        {
            StartCoroutine(MoveToStartPosition());
        }
    }

    IEnumerator MoveToEndPosition()
    {
        isMoving = true;

        //get current position
        Vector3 currentPos = transform.position;

        float fltTravelPercent = 0f;
        //Lerp to end position
        while (fltTravelPercent < 1)
        {
            transform.position = Vector3.Lerp(currentPos, endPos, fltTravelPercent);

            fltTravelPercent += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isMoving = false;
        moveToEnd = false;
    }

    IEnumerator MoveToStartPosition()
    {
        isMoving = true;

        //get current position
        Vector3 currentPos = transform.position;

        float fltTravelPercent = 0f;
        //Lerp to start position
        while (fltTravelPercent < 1)
        {
            transform.position = Vector3.Lerp(currentPos, startPos, fltTravelPercent);
            fltTravelPercent += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isMoving = false;
        moveToEnd = true;
    }
}
