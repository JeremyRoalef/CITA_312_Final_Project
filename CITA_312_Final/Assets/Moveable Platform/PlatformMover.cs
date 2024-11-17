using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * This script will be attached to the platform prefab.
 * 
 * This script will be responsible for accepting a bunch of positions the platform will move to and form using linear
 * interpolation 
 */

public class PlatformMover : MonoBehaviour
{
    //Serialized Fields
    [Header("The Path")]

    [SerializeField]
    [Tooltip("The path of platform positions in the world")]
    GameObject platformPath;

    [Header("Movement Attributes")]

    [SerializeField]
    [Range(0f,100f)]
    [Tooltip("How fast the platform will move through the path")]
    float fltPlatformVelocity = 1f;

    [SerializeField]
    [Min(0)]
    [Tooltip("The initial delay of the platform")]
    float fltInitialMoveDelay = 0f;

    [SerializeField]
    [Min(0)]
    [Tooltip("The delay before moving through next path")]
    float fltMoveDelay = 0f;

    //Cashe References
    List<Transform> path;

    //Attributes
    bool isMoving = false;
    bool isMovingForward = true;

    private void Awake()
    {
        path = new List<Transform>();

        //Add the child transform positions to the path
        foreach (Transform child in platformPath.transform)
        {
            path.Add(child);
        }
    }
    void Start()
    {
        StartCoroutine(FollowPath(true));
    }

    void Update()
    {
        if (!isMoving)
        {
            isMovingForward = !isMovingForward;
            StartCoroutine(FollowPath(isMovingForward));
        }
    }

    //TODO: if and else are the same but iterate in opposite directions. Can easily update this to use one iterator
    IEnumerator FollowPath(bool moveForward)
    {
        //Platform is moving
        isMoving = true;

        Debug.Log("Start of coroutine");

        if (moveForward)
        {
            //Set position to start
            transform.position = path[0].position;

            //Initial delay
            yield return new WaitForSeconds(fltInitialMoveDelay);

            for (int i = 1; i < path.Count; i++)
            {
                //Get start & end positions
                Vector3 startPos = transform.position;
                Vector3 endPos = path[i].position;

                //There's probably bloated variable usage in this that will eat up memory. Should try to find simpler
                //solution

                float distance = Vector3.Distance(startPos, endPos);

                //t = d/v 
                float time = distance / fltPlatformVelocity;
                float elapsedTime = 0f;
                float travelPercent = 0f;


                //If first path in list, set the transform and continue to next path object with no delay
                if (i == 0)
                {
                    transform.position = path[i].position;
                    while (travelPercent < 1)
                    {
                        transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                        //Travel % is proportional to elapsed time / total time
                        elapsedTime += Time.deltaTime;
                        travelPercent = elapsedTime / time;
                        yield return new WaitForEndOfFrame();
                    }
                    continue;
                }

                //case for when the path is not first
                while (travelPercent < 1)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    //Travel % is proportional to elapsed time / total time
                    elapsedTime += Time.deltaTime;
                    travelPercent = elapsedTime / time;
                    yield return new WaitForEndOfFrame();
                }

                //Delay between path
                yield return new WaitForSeconds(fltMoveDelay);
            }
        }

        else
        {
            //Set position to start
            transform.position = path[path.Count - 1].position;

            //Initial delay
            yield return new WaitForSeconds(fltInitialMoveDelay);

            for (int i = path.Count - 2; i >= 0; i--)
            {
                //Get start & end positions
                Vector3 startPos = transform.position;
                Vector3 endPos = path[i].position;

                //There's probably bloated variable usage in this that will eat up memory. Should try to find simpler
                //solution

                float distance = Vector3.Distance(startPos, endPos);

                //t = d/v 
                float time = distance / fltPlatformVelocity;
                float elapsedTime = 0f;
                float travelPercent = 0f;


                //If first path in list, set the transform and continue to next path object with no delay
                if (i == path.Count-1)
                {
                    transform.position = path[i].position;
                    while (travelPercent < 1)
                    {
                        transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                        //Travel % is proportional to elapsed time / total time
                        elapsedTime += Time.deltaTime;
                        travelPercent = elapsedTime / time;
                        yield return new WaitForEndOfFrame();
                    }
                    continue;
                }

                //case for when the path is not first
                while (travelPercent < 1)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    //Travel % is proportional to elapsed time / total time
                    elapsedTime += Time.deltaTime;
                    travelPercent = elapsedTime / time;
                    yield return new WaitForEndOfFrame();
                }

                //Delay between path
                yield return new WaitForSeconds(fltMoveDelay);
            }
        }

        Debug.Log("End of coroutine");
        //Platform is no longer moving
        isMoving = false;
    }
}
