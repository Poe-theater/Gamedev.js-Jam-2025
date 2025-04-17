using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    //[Header("References")]
    //[SerializeField] private CinemachineVirtualCamera cineCamera;

    //[Header("Zoom Modes (select one)")]
    //[SerializeField] private bool useFovZoom = false;
    //[SerializeField] private bool useForwardZoom = false;
    //[SerializeField] private bool useVerticalZoom = true;

    //[Header("FOV Zoom Settings")]
    //[SerializeField] private float zoomSpeedFov = 5f;
    //[SerializeField][Range(10f, 80f)] private float minFov = 10f;
    //[SerializeField][Range(10f, 80f)] private float maxFov = 80f;

    //[Header("Offset Zoom Settings")]
    //[SerializeField] private float zoomSpeedOffset = 10f;
    //[SerializeField] private float forwardMinDist = 10f;
    //[SerializeField] private float forwardMaxDist = 50f;
    //[SerializeField] private float verticalMinY = 10f;
    //[SerializeField] private float verticalMaxY = 50f;
    //[SerializeField] private float verticalStep = 3f;

    //[Header("Movement & Rotation")]
    //[SerializeField] private float moveSpeed = 50f;
    //[SerializeField] private float rotateSpeed = 300f;

    //private Vector3 followOffset;
    //private float targetFov;

    //private void Awake()
    //{
    //    cineFollow = cineCamera.GetComponent<CinemachineFollow>();
    //    followOffset = cineFollow.FollowOffset;
    //    targetFov = cineCamera.Lens.FieldOfView;
    //}

    //private void Update()
    //{
    //    HandleMovement();
    //    HandleRotation();
    //    HandleZoom();
    //}

    ///// <summary>
    ///// Chooses which zoom method to call based on Inspector toggles.
    ///// </summary>
    //private void HandleZoom()
    //{
    //    if (useFovZoom)
    //        HandleFovZoom();
    //    else if (useForwardZoom)
    //        HandleForwardZoom();
    //    else if (useVerticalZoom)
    //        HandleVerticalZoom();
    //}

    ///// <summary>
    ///// Zooms by adjusting the camera's field of view.
    ///// </summary>
    //private void HandleFovZoom()
    //{
    //    float scroll = Input.mouseScrollDelta.y;
    //    if (Mathf.Approximately(scroll, 0f)) return;

    //    targetFov = Mathf.Clamp(
    //        targetFov + scroll * zoomSpeedFov,
    //        minFov,
    //        maxFov
    //    );

    //    cineCamera.Lens.FieldOfView = targetFov;
    //}

    ///// <summary>
    ///// Zooms by moving the camera forward/back along its follow offset.
    ///// </summary>
    //private void HandleForwardZoom()
    //{
    //    Vector3 direction = followOffset.normalized;
    //    float scroll = Input.mouseScrollDelta.y;
    //    if (Mathf.Approximately(scroll, 0f)) return;

    //    // Move in or out along the follow direction
    //    followOffset += direction * scroll;

    //    // Clamp distance
    //    float mag = followOffset.magnitude;
    //    mag = Mathf.Clamp(mag, forwardMinDist, forwardMaxDist);
    //    followOffset = direction * mag;

    //    cineFollow.FollowOffset = Vector3.Lerp(
    //        cineFollow.FollowOffset,
    //        followOffset,
    //        Time.deltaTime * zoomSpeedOffset
    //    );
    //}

    ///// <summary>
    ///// Zooms by raising/lowering the camera's Y offset.
    ///// </summary>
    //private void HandleVerticalZoom()
    //{
    //    float scroll = Input.mouseScrollDelta.y;
    //    if (Mathf.Approximately(scroll, 0f)) return;

    //    followOffset.y = Mathf.Clamp(
    //        followOffset.y + scroll * -verticalStep,
    //        verticalMinY,
    //        verticalMaxY
    //    );

    //    cineFollow.FollowOffset = Vector3.Lerp(
    //        cineFollow.FollowOffset,
    //        followOffset,
    //        Time.deltaTime * zoomSpeedOffset
    //    );
    //}

    ///// <summary>
    ///// Moves the camera rig with WASD.
    ///// </summary>
    //private void HandleMovement()
    //{
    //    Vector3 input = Vector3.zero;
    //    if (Keyboard.current.wKey.isPressed) input.z += 1f;
    //    if (Keyboard.current.sKey.isPressed) input.z -= 1f;
    //    if (Keyboard.current.aKey.isPressed) input.x -= 1f;
    //    if (Keyboard.current.dKey.isPressed) input.x += 1f;

    //    Vector3 moveDir = transform.forward * input.z + transform.right * input.x;
    //    transform.position += moveDir * moveSpeed * Time.deltaTime;
    //}

    ///// <summary>
    ///// Rotates the camera rig around Y with Q/E.
    ///// </summary>
    //private void HandleRotation()
    //{
    //    float turn = 0f;
    //    if (Keyboard.current.eKey.isPressed) turn += 1f;
    //    if (Keyboard.current.qKey.isPressed) turn -= 1f;

    //    transform.Rotate(
    //        Vector3.up,
    //        turn * rotateSpeed * Time.deltaTime,
    //        Space.World
    //    );
    //}
}
