using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    float fltGravityActivationDelay = 0.2f;

    [SerializeField]
    [Min(0)]
    float fltDestroyDelay = 1f;

    [SerializeField]
    [Tooltip("The color the dropper will change to when it's getting ready to fall")]
    Color dropColor;

    public void InitiateDropSequence()
    {   
        StartCoroutine(ChangeDropperColor());
        Invoke("EnableGravity", fltGravityActivationDelay);
        Invoke("DestroyDropper", fltDestroyDelay);
    }

    //Used in drop sequence
    void EnableGravity()
    {
        transform.AddComponent<Rigidbody>();
    }
    //Used in drop sequence
    void DestroyDropper()
    {
        Destroy(gameObject);
    }

    IEnumerator ChangeDropperColor()
    {
        //store current color
        MeshRenderer objRenderer = GetComponentInChildren<MeshRenderer>();
        Color objColor = objRenderer.material.color;
        //change to target color
        float fltPercentChange = 0f;
        while (fltPercentChange < 1)
        {
            objRenderer.material.color = Color.Lerp(objColor, dropColor, fltPercentChange);
            fltPercentChange += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
