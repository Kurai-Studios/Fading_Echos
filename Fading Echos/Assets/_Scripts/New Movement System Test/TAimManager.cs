using UnityEngine;
using Unity.Cinemachine;
using System.Diagnostics;
using UnityEngine.Animations.Rigging;

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
    MultiAimConstraint[] multiAims;
    WeightedTransform aimPosWeightedT;

    [HideInInspector] public CinemachineCamera vCam;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;

    [Header("Aim Zoom")]
    public float adsFov = 40;
    public float fovSmootness = 10;

    [HideInInspector]public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;

    float xFollowPos;
    float yFollowPos;
    float ogYPos;
    [SerializeField] float crouchCamHeight = 0.6f;
    [SerializeField] float shoulderSwapSpeed = 10;
    TMovementManager moving;


    private void Awake()
    {
        aimPos = new GameObject().transform;
        aimPos.name = "AimPosition";

        aimPosWeightedT.transform = aimPos;
        aimPosWeightedT.weight = 1;

        multiAims = GetComponentsInChildren<MultiAimConstraint>();

        foreach (MultiAimConstraint constraint in multiAims)
        {
            var data = constraint.data.sourceObjects;
            data.Clear();
            data.Add(aimPosWeightedT);
            constraint.data.sourceObjects = data;
        }
    }

    void Start()
    {
        moving = GetComponent<TMovementManager>();
        xFollowPos = camFollowPos.localPosition.x;
        ogYPos = camFollowPos.localPosition.y;
        yFollowPos = ogYPos;
        vCam = GetComponentInChildren<CinemachineCamera>();
        hipFov = vCam.Lens.FieldOfView;
        animator = GetComponent<Animator>();
        SwitchState(Hip);
    }

    void Update()
    {

        float mouseX = Input.GetAxis(horizontalInput);
        float mouseY = Input.GetAxis(verticalInput);

        if (invertY)
            mouseY = -mouseY;

        xAxis.Value += mouseX * xSpeed * Time.deltaTime;
        yAxis.Value += mouseY * ySpeed * Time.deltaTime;

        yAxis.Value = Mathf.Clamp(yAxis.Value, minYAngle, maxYAngle);

        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, currentFov, fovSmootness * Time.deltaTime);

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }

        MoveCamera();

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

    void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            xFollowPos = -xFollowPos; 
        }

        if (moving.currentState == moving.TCrouch)
        {
            yFollowPos = crouchCamHeight;
        }
        else
        {
            yFollowPos = ogYPos;
        }

        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, camFollowPos.localPosition.z);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);

    }
}
