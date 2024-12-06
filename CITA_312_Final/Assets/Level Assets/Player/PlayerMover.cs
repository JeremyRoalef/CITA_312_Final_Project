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

    [SerializeField] 
    float fltGroundVelocity;

    [SerializeField]
    float fltAirVelocity;

    [SerializeField] 
    InputAction movementInput;

    [Header("Jumping Attributes")]

    [SerializeField] 
    InputAction jumpInput;
    
    [SerializeField] 
    [Min(0)] 
    float fltJumpForce = 100f;
    
    [SerializeField] 
    [Min(0)] 
    float maxVelocity = 10f;

    [SerializeField]
    [Min(0)]
    float fltJumpLockoutDuration = 1f;

    //Cashe References
    Rigidbody playerRb;
    Camera mainCam;
    CapsuleCollider playerCollider;

    //Attributes
    bool canJump = false;
    bool isGrounded = false;
    bool inJumpLockout = false;

    void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
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

        playerCollider = GetComponentInChildren<CapsuleCollider>();
        if (playerCollider == null)
        {
            Debug.Log("No capsule collider found in children");
        }
        else
        {
            Debug.Log("Capsule collider found in children");
        }
    }

    void OnEnable()
    {
        movementInput.Enable();
        jumpInput.Enable();
    }

    void OnDisable()
    {
        movementInput.Disable();
        jumpInput.Disable();
    }


    void Update()
    {
        PlayerMoves();
        PlayerJumps();
    }

    void PlayerMoves()
    {
        //Do not run if the movement input is deactivated
        if (movementInput.enabled == false)
        {
            return;
        }

        //if (!isGrounded)
        //{
        //    return;
        //}

        //Read the input values of the movement input
        Vector2 inputDir = movementInput.ReadValue<Vector2>();
        //Debug.Log($"Movement direction: {inputDir}");

        //Direction of movement is relative to the cinemachine's forward direction.
        //Thus, moving the player "forward" is relative to the cinemachine's forward direction

        //Get the main camera's forwards vector
        Vector2 forwardDir = new Vector2(
            mainCam.transform.forward.x,
            mainCam.transform.forward.z
            ).normalized;
        Vector2 rightDir = new Vector2(
            mainCam.transform.right.x,
            mainCam.transform.right.z
            ).normalized;

        //Debug.Log($"Camera's forward direction = {forwardDir}");
        //Debug.Log($"Camera's right direction = {rightDir}");

        //Logic: A/D moves left/right of the left direction.
        //       W/S moves up/down of the forward direction.

        //Vector direction to move
        Vector3 moveDir = new Vector3(0, 0, 0);

        if (isGrounded)
        {
            if (Mathf.Abs(inputDir.x) > Mathf.Epsilon)
            {
                //Add rightDir to the moveDir
                moveDir += new Vector3(rightDir.x * inputDir.x, 0, rightDir.y * inputDir.x) * fltGroundVelocity;
            }
            if (Mathf.Abs(inputDir.y) > Mathf.Epsilon)
            {
                moveDir += new Vector3(forwardDir.x * inputDir.y, 0, forwardDir.y * inputDir.y) * fltGroundVelocity;
            }
        }

        else
        {
            if (Mathf.Abs(inputDir.x) > Mathf.Epsilon)
            {
                //Add rightDir to the moveDir
                moveDir += new Vector3(rightDir.x * inputDir.x, 0, rightDir.y * inputDir.x) * fltAirVelocity;
            }
            if (Mathf.Abs(inputDir.y) > Mathf.Epsilon)
            {
                moveDir += new Vector3(forwardDir.x * inputDir.y, 0, forwardDir.y * inputDir.y) * fltAirVelocity;
            }
        }


        //moveDir.y = playerRb.velocity.y;
        
        playerRb.AddForce(moveDir);
        //Debug.Log(playerRb.velocity);

        //Clamp the min & max velocity for x and z velocity if on the ground

        if (isGrounded)
        {
            playerRb.velocity = new Vector3(
                Mathf.Clamp(playerRb.velocity.x, -maxVelocity, maxVelocity),
                playerRb.velocity.y,
                Mathf.Clamp(playerRb.velocity.z, -maxVelocity, maxVelocity)
                );
        }

        //Debug.Log($"Player velocity: {playerRb.velocity}");
        Debug.DrawRay(transform.position, playerRb.velocity);
    }
    
    void PlayerJumps()
    {
        if (jumpInput.ReadValue<float>() > Mathf.Epsilon && CheckIfPlayerCanJump())
        {
            playerRb.AddForce(0,fltJumpForce,0);
            canJump = false;
            LockJump();
        }
        //else
        //{
        //    Debug.Log("Not jumping, Reason:");
        //    Debug.Log($"Jump input: {jumpInput.ReadValue<float>() > Mathf.Epsilon}");
        //    Debug.Log($"Can jump: {canJump}");
        //}
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collision with " + other.gameObject.name + " Tagged " + other.gameObject.tag);

        //Check if colliding with ground object
        switch (other.gameObject.tag)
        {
            //Add cases here for jump logic
            case "Ground":
                canJump = true;
                isGrounded = true;
                break;
            default:
                //Debug.Log("Collision detected, not doing anything");
                break;
        }
    }
    void OnCollisionStay(Collision other)
    {
        //Check if colliding with ground object
        switch (other.gameObject.tag)
        {
            //Add cases here for jump logic
            case "Ground":
                canJump = true;
                isGrounded = true;
                break;
            default:
                //Debug.Log("Collision stayed, not doing anything");
                break;
        }
    }
    void OnCollisionExit(Collision other)
    {
        //Check if colliding with ground object
        switch (other.gameObject.tag)
        {
            //Add cases here for jump logic
            case "Ground":
                isGrounded = false;
                break;
            default:
                //Debug.Log("Exiting collision, not doing anything");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dangerous"))
        {
            KillPlayer();
        }
    }

    public void LockMovement(float lockoutDuration)
    {
        movementInput.Disable();
        Invoke("EnableMovement", lockoutDuration);
    }

    void KillPlayer()
    {
        Debug.Log("Time to kill the player :)");
    }

    void EnableMovement()
    {
        movementInput.Enable();
    }

    bool CheckIfPlayerCanJump()
    {
        //Return the conditions for player jumping.
        //If player can jump and they are not locked out of jumping, this'll return true
        return isGrounded && canJump && !inJumpLockout;
    }

    void LockJump()
    {
        inJumpLockout = true;
        Invoke("UnlockJump", fltJumpLockoutDuration);
    }

    void UnlockJump()
    {
        inJumpLockout = false;
    }
}
