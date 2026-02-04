using UnityEngine;
using Unity.Cinemachine;

public class TAimManager : MonoBehaviour
{
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

    void Start()
    {
        
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
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
