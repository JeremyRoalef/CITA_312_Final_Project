using UnityEngine;
using UnityEngine.InputSystem;

/*
 * This script is attached to the player prefab.
 * 
 * This script is responsible for moving the player relative to the camera that is looking at the player. This script
 * will also be responsible for other mechanics involving player movemnt like jumping, running, sprinting, etc.
 */

//Script requires rigigbody and audio source components
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerMover : MonoBehaviour
{
    //Serialized Fields
    [Header("Movemenet Attributes")]

    [SerializeField]
    [Tooltip("The speed the player will move at when grounded")]
    float fltGroundVelocity;

    [SerializeField]
    [Tooltip("The speed the player will move at when not grounded")]
    float fltAirVelocity;

    [SerializeField] 
    InputAction movementInput;


    [Header("Jumping Attributes")]

    [SerializeField] 
    InputAction jumpInput;
    
    [SerializeField] 
    [Min(0)]
    [Tooltip("The strength of the player's jump")]
    float fltJumpForce = 100f;
    
    [SerializeField] 
    [Min(0)]
    [Tooltip("The maximum ground speed the player can move at")]
    float maxVelocity = 10f;

    [SerializeField]
    [Min(0)]
    [Tooltip("The minimum delay between two jump inputs")]
    float fltJumpLockoutDuration = 1f;

    //Cashe References
    Rigidbody playerRb;
    Camera mainCam;
    AudioSource audioSource;

    //Attributes
    bool canJump = false;
    bool isGrounded = false;
    bool inJumpLockout = false;

    //Event Systems
    void Awake()
    {
        InitializeReferences();
    }

    void OnEnable()
    {
        //Enable input bindings
        movementInput.Enable();
        jumpInput.Enable();
    }

    void OnDisable()
    {
        //Disable input bindings
        movementInput.Disable();
        jumpInput.Disable();
    }

    void Update()
    {
        PlayerMoves();
        PlayerJumps();
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
                //Debug.Log("Collision stayed, not doing anything");
                break;
        }
    }

    void OnCollisionExit(Collision other)
    {
        //Debug.Log("Collision with " + other.gameObject.name + " Tagged " + other.gameObject.tag);

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dangerous"))
        {
            KillPlayer();
        }
    }


    //Public Methods
    public void LockMovement(float lockoutDuration)
    {
        movementInput.Disable();
        Invoke("EnableMovement", lockoutDuration);
    }


    //Private Mehtods
    void InitializeReferences()
    {
        playerRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

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

    void PlayerMoves()
    {
        /*
         * Logic:
         * 
         * 1)
         * Do not run if the movement input is deactivated
         * 
         * 2)
         * Get the main camera's forward and right vector & normalize them to remove magnitude
         * 
         * 3)
         * Direction of movement is relative to the cinemachine's looking direction.
         * Thus, moving the player "forward" is relative to the cinemachine's forward direction
         * and moving the player "rightward" is relative to the cinemachine's rightward direction
         * Additionally, player moves quicker and slower based on whether they are grounded or not.
         * 
         * 4)
         * Clamp the min & max velocity for x and z velocity if on the ground
         */


        //1)
        if (movementInput.enabled == false) {return;}

        //Read the input values of the movement input
        Vector2 inputDir = movementInput.ReadValue<Vector2>();

        //2)
        Vector2 forwardDir = new Vector2(
            mainCam.transform.forward.x,
            mainCam.transform.forward.z
            ).normalized;
        Vector2 rightDir = new Vector2(
            mainCam.transform.right.x,
            mainCam.transform.right.z
            ).normalized;

        //Direction to move
        Vector3 moveDir = new Vector3(0, 0, 0);

        //3)
        if (isGrounded)
        {
            if (Mathf.Abs(inputDir.x) > Mathf.Epsilon)
            {
                //Add rightDir to the moveDir
                moveDir += new Vector3(rightDir.x * inputDir.x, 0, rightDir.y * inputDir.x) * fltGroundVelocity;
            }
            if (Mathf.Abs(inputDir.y) > Mathf.Epsilon)
            {
                //Add forwardDir to the moveDir
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
        
        //Apply force to the player
        playerRb.AddForce(moveDir);

        //4)
        if (isGrounded)
        {
            playerRb.velocity = new Vector3(
                Mathf.Clamp(playerRb.velocity.x, -maxVelocity, maxVelocity),
                playerRb.velocity.y,
                Mathf.Clamp(playerRb.velocity.z, -maxVelocity, maxVelocity)
                );
        }

        //Debug movement

        //Debug.Log($"Player velocity: {playerRb.velocity}");
        //Debug.Log($"Movement direction: {inputDir}");
        //Debug.Log($"Camera's forward direction = {forwardDir}");
        //Debug.Log($"Camera's right direction = {rightDir}");
        //Debug.Log($"Player velocity: {playerRb.velocity}");
        //Debug.DrawRay(transform.position, playerRb.velocity);
    }

    void PlayerJumps()
    {
        if (jumpInput.ReadValue<float>() > Mathf.Epsilon && CheckIfPlayerCanJump())
        {
            playerRb.AddForce(0,fltJumpForce,0);
            canJump = false;
            LockJump();
            PlayJumpSound();
        }
    }

    void KillPlayer()
    {
        Debug.Log("Time to kill the player");
    }

    //Method is invoked in LockMovement method
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

    //Called in LockJump method
    void UnlockJump()
    {
        inJumpLockout = false;
    }

    void PlayJumpSound()
    {
        //Stop any previous sound played
        audioSource.Stop();
        audioSource.Play();
    }
}
