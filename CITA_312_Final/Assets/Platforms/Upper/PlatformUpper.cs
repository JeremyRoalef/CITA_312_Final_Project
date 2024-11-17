using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpper : MonoBehaviour
{
    //Serialized Fields
    [SerializeField]
    Vector3 targetPos;

    //Cashe References

    //Attributes
    Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        StopAllCoroutines();

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveToTargetPosition());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        StopAllCoroutines();

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveToStartPosition());
        }
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
