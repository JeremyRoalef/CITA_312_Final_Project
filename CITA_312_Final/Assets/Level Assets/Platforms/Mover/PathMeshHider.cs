using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the platform path object.
 * 
 * This script is responsible for hiding and showing the path in editor view.
 */

[ExecuteAlways]
public class PathMeshHider : MonoBehaviour
{
    //Serialized fields
    [SerializeField]
    bool showPath = true;

    void Start()
    {
        if (Application.isPlaying)
        {
            HideMeshRenderers();
            //Get rid of this component when the game is running. It'll take up memory
            Destroy(this);
        }
    }

    void Update()
    {
        if (Application.isPlaying) { return; }

        if (showPath)
        {
            ShowMeshRenderers();
        }
        else
        {
            HideMeshRenderers();
        }
    }
    void HideMeshRenderers()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    void ShowMeshRenderers()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
