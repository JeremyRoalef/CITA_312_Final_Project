using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * This script will be attached to the platform prefab.
 * 
 * This script will be responsible for accepting a bunch of positions the platform will move to and form using linear
 * interpolation 
 */

public class PlatformMover : MonoBehaviour
{
    //Serialized Fields
    [SerializeField] [Tooltip("The path of platform positions in the world")]
    GameObject platformPath;
    [SerializeField][Range(0f,100f)][Tooltip("How fast the platform will move through the path")]
    float fltPlatformVelocity = 1f;
    [SerializeField] [Min(0)] [Tooltip("The initial delay of the platform")]
    float fltInitialMoveDelay = 0f;
    [SerializeField] [Min(0)] [Tooltip("The delay before moving through next path")]
    float fltMoveDelay = 0f;

    //Cashe References
    List<Transform> path;

    //Attributes
    bool isMoving = false;

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
        StartCoroutine(FollowPath(false));
    }

    void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(FollowPath(true));
        }
    }
    IEnumerator FollowPath(bool reverseList)
    {
        //If the path is to move backwards, reverse the order of the elements
        //May want to change where you iterate backwards in the list instead
        if (reverseList)
        {
            path.Reverse();
        }

        Debug.Log("start of coroutine");
        isMoving = true;

        //Initial delay
        yield return new WaitForSeconds(fltInitialMoveDelay);

        //Set starting position
        transform.position = path[0].transform.position;


        foreach (Transform position in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = position.position;

            //There's probably bloated variable usage in this that will eat up memory. Should try to find simpler
            //solution

            float distance = Vector3.Distance(startPos, endPos);

            //t = d/v 
            float time = distance / fltPlatformVelocity;
            float elapsedTime = 0f;
            float travelPercent = 0f;

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

        Debug.Log("End of coroutine");
        isMoving = false;
    }
}
