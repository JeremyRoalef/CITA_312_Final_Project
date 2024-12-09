using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the upper prefab.
 * 
 * This script will be responsible for upper behavior
 */
public class Upper : MonoBehaviour
{
    //Serialized Fields
    [SerializeField]
    Vector3 targetPos;

    //Attributes
    Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    //Called in upper collision handler script
    public void CallMoveToStartCoroutine()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToStartPosition());
    }
    //Called in upper collision handler script
    public void CallMoveToTargetCoroutine()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTargetPosition());
    }

    IEnumerator MoveToTargetPosition()
    {
        //get current position
        Vector3 currentPos = transform.position;

        float fltTravelPercent = 0f;
        //Lerp to end position
        while (fltTravelPercent < 1)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, fltTravelPercent);

            fltTravelPercent += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator MoveToStartPosition()
    {
        //get current position
        Vector3 currentPos = transform.position;

        float fltTravelPercent = 0f;
        //Lerp to end position
        while (fltTravelPercent < 1)
        {
            transform.position = Vector3.Lerp(currentPos, startPos, fltTravelPercent);
            fltTravelPercent += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
