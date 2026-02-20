using UnityEngine;


public class TMovementManager : MonoBehaviour
{
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 5, runBackSpeed = 4;
    public float crouchSpeed = 2, crouchBackSpeed = 1;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hzInput;
    [HideInInspector] public float vInput;
    CharacterController valerieController;

    [SerializeField] float groundYOffSet;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    MovementState currentState;

    public IdleState TIdle = new IdleState();
    public WalkState TWalk = new WalkState();
    public RunState TRun = new RunState();
    public CrouchState TCrouch = new CrouchState();

    [HideInInspector] public Animator TAnimator;

    void Start()
    {
        TAnimator = GetComponent<Animator>();
        valerieController = GetComponent<CharacterController>();
        SwitchState(TIdle);
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        TAnimator.SetFloat("hzInput", hzInput);
        TAnimator.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        valerieController.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffSet, transform.position.z);
        
        if (Physics.CheckSphere(spherePos, valerieController.radius -0.05f, groundMask))
        {
            return true;
        }
     
            return false;
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }

        valerieController.Move(velocity * Time.deltaTime);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, valerieController.radius - 0.05f);
    }*/
}
