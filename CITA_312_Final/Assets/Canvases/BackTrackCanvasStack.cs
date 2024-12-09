using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class will be used to traverse canvases in the same scene. This class is not attached to any game objects.
 */

public class BackTrackCanvasStack
{
    //Static stack of canvases
    static Stack<GameObject> previousCanvas = new Stack<GameObject>();

    public static void AddCanvasToStack(GameObject currentCanvas)
    {
        //Push canvas to stack
        previousCanvas.Push(currentCanvas);
    }

    public static void ReturnToPreviousCanvas(GameObject currentCanvas)
    {
        if (previousCanvas.Count > 0)
        {
            //Reveal the previous canvas
            previousCanvas.Pop().SetActive(true);

            //Hide the current canvas
            currentCanvas.SetActive(false);
        }
        //This should NEVER run, but added just in case
        else
        {
            Debug.Log("Somehow, there's no canvas in the stack");
        }
    }
}
