using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    MovementManager movementManager;
    AnimatorManager animatorManager;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool run_Input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        movementManager = GetComponent<MovementManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Run.performed += i => run_Input = true;
            playerControls.PlayerActions.Run.canceled += i => run_Input = false;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleRunningInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, movementManager.isRunning);
    }

    private void HandleRunningInput()
    {
        if (run_Input && moveAmount > 0.5f)
        {
            movementManager.isRunning = true;
        }
        else
        {
            movementManager.isRunning = false;
        }
    }
}
