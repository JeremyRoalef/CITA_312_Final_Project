using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * This script is attached to the player prefab.
 * 
 * This script will be responsible for moving the player based on the perspective the camera that is
 * looking at the player. This script will also be responsible for other mechanics involving player movement
 * like jumping, running, sprinting, etc.
 */

public class PlayerMover : MonoBehaviour
{
    //Serialized Fields
    [Header("Movemenet Attributes")]
    [SerializeField] float fltMoveSpeed;
    [SerializeField] InputAction movementInput;

    //Cashe References
    Rigidbody playerRb;
    Camera mainCam;

    //Attributes


    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        if (playerRb == null)
        {
            Debug.Log("No Rigidbody found");
        }
        else
        {
            Debug.Log("Player Rigidbody found");
        }

        mainCam = FindObjectOfType<Camera>();
        if (mainCam == null)
        {
            Debug.Log("No camera found");
        }
        else
        {
            Debug.Log("Camera found");
        }
    }

    void OnEnable()
    {
        movementInput.Enable();
    }

    void OnDisable()
    {
        movementInput.Disable();
    }


    void Update()
    {
        //Read the input values of the movement input
        Vector2 inputDir = movementInput.ReadValue<Vector2>();
        Debug.Log($"Movement direction: {inputDir}");

        //Direction of movement is relative to the cinemachine's forward direction.
        //Thus, moving the player "forward" is relative to the cinemachine's forward direction

        //Get the main camera's forwards vector
        Vector2 forwardDir = new Vector2(
            mainCam.transform.forward.x,
            mainCam.transform.forward.z
            );
        Vector2 rightDir = new Vector2(
            mainCam.transform.right.x,
            mainCam.transform.right.z
            );

        Debug.Log($"Camera's forward direction = {forwardDir}");
        Debug.Log($"Camera's right direction = {rightDir}");

        //Logic: A/D moves left/right of the left direction.
        //       W/S moves up/down of the forward direction.

        //Vector direction to move
        Vector3 moveDir = new Vector3(0,0,0);

        if (Mathf.Abs(inputDir.x) > Mathf.Epsilon)
        {
            //Add rightDir to the moveDir
            moveDir += new Vector3(rightDir.x * inputDir.x, 0, rightDir.y * inputDir.x) * fltMoveSpeed;
        }
        if (Mathf.Abs(inputDir.y) > Mathf.Epsilon)
        {
            moveDir += new Vector3(forwardDir.x * inputDir.y, 0, forwardDir.y * inputDir.y) * fltMoveSpeed;
        }

        moveDir.y = playerRb.velocity.y;

        playerRb.velocity = moveDir;
    }
}
