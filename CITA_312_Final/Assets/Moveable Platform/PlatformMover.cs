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


    //Cashe References
    List<Transform> path;

    //Attributes

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


        StartCoroutine(FollowPath());
    }

    void Update()
    {

    }
    IEnumerator FollowPath()
    {
        Debug.Log("start of coroutine");

        //Set starting position
        transform.position = path[0].transform.position;


        foreach (Transform position in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = position.position;

            float travelPercent = 0f;
            while (travelPercent < 1)
            {
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                travelPercent += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        Debug.Log("End of coroutine");
    }
}
