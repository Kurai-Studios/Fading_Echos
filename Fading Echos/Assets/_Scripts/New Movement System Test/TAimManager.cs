using UnityEngine;
using Unity.Cinemachine;
using System.Diagnostics;

public class TAimManager : MonoBehaviour
{
    TAimBaseManager currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    public InputAxis xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    [Header("Speed Settings")]
    [SerializeField] private float xSpeed = 300f;
    [SerializeField] private float ySpeed = 300f;

    [Header("Input Settings")]
    [SerializeField] private string horizontalInput = "Mouse X";
    [SerializeField] private string verticalInput = "Mouse Y";

    private bool invertY = true;

    [Header("Look Limits")]
    [SerializeField] private float minYAngle = -35f;
    [SerializeField] private float maxYAngle = 35f;

    [HideInInspector] public Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        SwitchState(Hip);
    }

    void Update()
    {
        // Get input
        float mouseX = Input.GetAxis(horizontalInput);
        float mouseY = Input.GetAxis(verticalInput);

        if (invertY)
            mouseY = -mouseY;

        xAxis.Value += mouseX * xSpeed * Time.deltaTime;
        yAxis.Value += mouseY * ySpeed * Time.deltaTime;

        yAxis.Value = Mathf.Clamp(yAxis.Value, minYAngle, maxYAngle);

        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }

    public void SwitchState(TAimBaseManager state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
