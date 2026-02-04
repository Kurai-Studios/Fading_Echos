using UnityEngine;

public class MovementManager : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody rb;

    [Header("Falling")]
    public float inAirTimer;
    public float fallingVelocity = 25f;
    public float rayCastHeightOffSett = 0.5f;
    public LayerMask groundLayer;
    public float maxDistance = 1;
    public float groundCheckRadius = 0.3f;


    [Header ("Movement Flags")]
    public bool isRunning;
    public bool isGrounded;
    public bool isJumping;

    [Header ("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5f;
    public float rotationSpeed = 15f;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;
    public float airControl = 0.5f;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

        rb.useGravity = false;
        rb.isKinematic = false;
    }

    private void FixedUpdate()
    {
        HandleFallingAndLanding();
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting)
        {
            return;
        }

        /*if (isJumping)
        {
            return;
        }*/

        HandleMovement();
        HandleRotation();
        //HandleJumping();
    }

    private void HandleMovement()
    {
        if (isJumping)
        {
            //HandleAirControl();
            return;
        }

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isRunning)
        {
            moveDirection = moveDirection * runningSpeed;
        }
        else
        {
            moveDirection = moveDirection * walkingSpeed;
        }

        Vector3 movementVelocity = moveDirection;
        rb.linearVelocity = movementVelocity;
    }

    /*private void HandleAirControl()
    {
        Vector3 airDirection = cameraObject.forward * inputManager.verticalInput;
        airDirection += cameraObject.right * inputManager.horizontalInput;
        airDirection.y = 0;

        if (airDirection.magnitude > 0.1f)
        {
            airDirection.Normalize();
            float airSpeed = isRunning ? runningSpeed * 0.5f : walkingSpeed * 0.5f;
            Vector3 airVelocity = airDirection * airSpeed;

            rb.linearVelocity = new Vector3(airVelocity.x, rb.linearVelocity.y, airVelocity.z);
        }
    }*/

    private void HandleRotation()
    {
        if (isJumping)
        {
            return;
        }

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize(); 
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        bool wasGrounded = isGrounded;

        Vector3 sphereCastOrigin = transform.position;
        sphereCastOrigin.y += rayCastHeightOffSett;
        RaycastHit hit;
        bool sphereCastHit = Physics.SphereCast(sphereCastOrigin, groundCheckRadius, -Vector3.up, out hit,
                                                maxDistance + rayCastHeightOffSett, groundLayer);

        bool feetCheck = Physics.CheckSphere(transform.position - new Vector3(0, 0.1f, 0), groundCheckRadius, groundLayer);
        
        isGrounded = sphereCastHit || feetCheck;

        if (isGrounded && isJumping && rb.linearVelocity.y <= 0)
        {
            isJumping = false;
            animatorManager.animator.SetBool("isJumping", false);
        }

        Debug.DrawRay(sphereCastOrigin, -Vector3.up * (maxDistance + rayCastHeightOffSett),
                     isGrounded ? Color.green : Color.red);

        if (!wasGrounded && isGrounded)
        {
            Debug.Log("LANDED - Playing Landing animation");
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            inAirTimer = 0;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        }
        else if (wasGrounded && !isGrounded && !isJumping)
        {
            Debug.Log("STARTED FALLING - Playing Fall animation");
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Fall", true);
            }
        }

        if (!isGrounded || isJumping)
        {
            inAirTimer += Time.deltaTime;

            if (!isJumping || rb.linearVelocity.y <= 0)
            {
                float gravityForce = fallingVelocity * inAirTimer * Time.deltaTime;
                rb.linearVelocity += Vector3.down * gravityForce;
            }

            float maxFallSpeed = 50f;

            if (rb.linearVelocity.y < -maxFallSpeed)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, -maxFallSpeed, rb.linearVelocity.z);
            }

            Debug.Log($"In Air - Velocity Y: {rb.linearVelocity.y}, isJumping: {isJumping}, inAirTimer: {inAirTimer}");
        }
        else
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -2f, rb.linearVelocity.z);
            inAirTimer = 0;
        }
    }

    public void HandleJumping()
    {
        if (isGrounded && !isJumping)
        {
            isJumping = true;
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.linearVelocity = new Vector3(horizontalVelocity.x, jumpingVelocity, horizontalVelocity.z);
            inAirTimer = 0;

            Debug.Log($"JUMPING! Velocity: {jumpingVelocity}");
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Draw ground check sphere
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, 0.1f, 0), groundCheckRadius);

        // Draw sphere cast
        Vector3 origin = transform.position + new Vector3(0, rayCastHeightOffSett, 0);
        Gizmos.DrawWireSphere(origin, groundCheckRadius);
        Gizmos.DrawLine(origin, origin - new Vector3(0, maxDistance + rayCastHeightOffSett, 0));
    }
}
