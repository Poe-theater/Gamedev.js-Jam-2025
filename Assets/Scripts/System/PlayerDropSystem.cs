using Cinemachine;
using System;
using UnityEngine;

public class PlayerDropSystem : MonoBehaviour
{
    [SerializeField] private Transform cameraSystem;
    [SerializeField] private Camera cam;
    [SerializeField] private float forceMultiplier = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool isDragging;
    [SerializeField] private Vector3 previousDragPos;
    [SerializeField] private Vector3 dragVelocity;
    [SerializeField] private GameObject sphere;
    [SerializeField] private LayerMask groundLayer;

    public Transform block;
    public Vector3 targetPos;
    public float distanceFromGrid = 50f;
    public event EventHandler OnBlockDrop;
    public event EventHandler OnBlockGrab;

    public Transform quadBlocker;

    /// <summary>
    /// Processes user input each frame. Checks for drag, release, and rotation inputs.
    /// </summary>
    public void Loop()
    {
        if (Input.GetMouseButtonDown(0)) BeginDrag();
        else if (Input.GetMouseButton(0) && isDragging) Drag();
        else if (Input.GetMouseButtonUp(0)) EndDrag();
        if (Input.GetKeyDown(KeyCode.Z)) block.Rotate(0, 90, 0);
        if (Input.GetKeyDown(KeyCode.X)) block.Rotate(0, 0, 90);
    }


    /// <summary>
    /// Prepares a block for dragging. Sets the block reference and caches its Rigidbody.
    /// </summary>
    /// <param name="newBlock">The block to manipulate.</param>
    public void SetBlock(Transform newBlock)
    {
        block = newBlock;
        if (!block.TryGetComponent(out rb))
            Debug.LogError("The selected block does not have a Rigidbody component.");
        else
            rb.isKinematic = true;
    }

    /// <summary>
    /// Initiates the dragging process by calculating the offset between the block and the mouse's world position.
    /// </summary>
    private void BeginDrag()
    {
        sphere.SetActive(true);
        OnBlockGrab?.Invoke(this, EventArgs.Empty);
        previousDragPos = block.position;
        isDragging = true;
        SetSpherePos();
    }

    /// <summary>
    /// Handles the dragging process by moving the block based on the mouse's world position.
    /// Calculates drag velocity for applying momentum when released.
    /// </summary>
    private void Drag()
    {
        targetPos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 blockPos = hit.point;
            blockPos.y += distanceFromGrid;
            block.transform.position = blockPos;
        }

        //rb.MovePosition(targetPos);
        
        SetSpherePos();
        
        dragVelocity = (targetPos - previousDragPos) / Time.deltaTime;
        
        previousDragPos = targetPos;
    }

    /// <summary>
    /// Ends the dragging process. Releases the block, applies momentum force, and notifies listeners.
    /// </summary>
    private void EndDrag()
    {
        sphere.SetActive(false);
        
        BoxCollider[] colliders = block.GetComponents<BoxCollider>();
        foreach (var col in colliders)
            col.enabled = true;

        OnBlockDrop?.Invoke(this, EventArgs.Empty);
        SetSpherePos();
        isDragging = false;
        rb.isKinematic = false;
        rb.AddForce(dragVelocity * forceMultiplier, ForceMode.VelocityChange);
        block = null;
    }
    private void SetSpherePos()
    {
        Vector3 startRaycast = block.position;

        if (Physics.Raycast(block.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            sphere.transform.position = hit.point;
    }
}
