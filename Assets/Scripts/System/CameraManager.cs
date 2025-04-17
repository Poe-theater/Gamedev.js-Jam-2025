using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private CinemachineCamera cineCamera;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float maxZoom = 50f;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float followOffsetMin = 5f;
    [SerializeField] private float followOffsetMax = 50f;

    private Vector3 followOffset;
    private CinemachineFollow cineFollow;
    private float targetFOV = 50;

    private void Start()
    {
        cineFollow = cineCamera.GetComponent<CinemachineFollow>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleFovZoom();
    }

    private void HandleCameraZoomForward()
    {
        if (cineFollow == null) return;

        Vector3 offset = cineFollow.FollowOffset;
        float scrollInput = Input.mouseScrollDelta.y;

        // Adjust the Z-axis of the offset for zooming
        offset.z += scrollInput * zoomSpeed * Time.deltaTime;
        offset.z = Mathf.Clamp(offset.z, -maxZoom, -minZoom); // Assuming negative Z moves the camera forward

        cineFollow.FollowOffset = offset;
    }

    private void HandleFovZoom() 
    {
        if (Input.mouseScrollDelta.x > 0)
            targetFOV += 5;
        if (Input.mouseScrollDelta.x < 0)
            targetFOV -= 5;

        //targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);

        cineCamera.Lens.FieldOfView = targetFOV;
    }

    private void HandleMovement()
    {
        Vector3 inputDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1;

        float rotateSpeed = 300f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }
}
