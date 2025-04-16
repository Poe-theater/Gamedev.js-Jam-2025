using System;
using UnityEngine;

public class PlayerDropSystem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float forceMultiplier = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool isDragging;
    [SerializeField] private Vector3 previousDragPos;
    [SerializeField] private Vector3 dragVelocity;
    [SerializeField] private Transform sphere;
    public Transform block { get; private set; }
    public event EventHandler OnBlockDrop;

    public Transform quadBlocker;

    /// <summary>
    /// Processes user input each frame. Checks for drag, release, and rotation inputs.
    /// </summary>
    public void Loop()
    {
        if (Input.GetMouseButtonDown(0)) BeginDrag();
        else if (Input.GetMouseButton(0) && isDragging) Drag();
        else if (Input.GetMouseButtonUp(0)) EndDrag();
        if (Input.GetKeyDown(KeyCode.Q)) block.Rotate(0, 90, 0);
        if (Input.GetKeyDown(KeyCode.E)) block.Rotate(0, 0, 90);
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
        offset = block.position - GetMouseWorldPosition();
        previousDragPos = block.position;
        isDragging = true;
    }

    /// <summary>
    /// Handles the dragging process by moving the block based on the mouse's world position.
    /// Calculates drag velocity for applying momentum when released.
    /// </summary>
    private void Drag()
    {
        Vector3 targetPos = GetMouseWorldPosition() + offset;
        targetPos.y = quadBlocker.position.y + 5;
        rb.MovePosition(targetPos);

        RaycastHit hit;
        if (Physics.Raycast(block.position, (block.TransformDirection(Vector3.down)), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Block"))
            {
                //Renderer renderer = hit.transform.GetComponent<Renderer>();
                sphere.position = new Vector3(targetPos.x, hit.point.y, targetPos.z);
            }
            else
                sphere.position = new Vector3(targetPos.x, hit.collider.transform.position.y, targetPos.z);
        }

        dragVelocity = (targetPos - previousDragPos) / Time.deltaTime;
        previousDragPos = targetPos;
    }

    /// <summary>
    /// Ends the dragging process. Releases the block, applies momentum force, and notifies listeners.
    /// </summary>
    private void EndDrag()
    {
        OnBlockDrop?.Invoke(this, EventArgs.Empty);

        isDragging = false;
        rb.isKinematic = false;
        rb.AddForce(dragVelocity * forceMultiplier, ForceMode.VelocityChange);

        block = null;
    }

    /// <summary>
    /// Converts the current mouse position to a point in world space based on a horizontal drag plane.
    /// </summary>
    /// <returns>The calculated world position.</returns>
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane dragPlane = new (Vector3.up, new Vector3(0, quadBlocker.position.y, 0));

        if (dragPlane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return block != null ? block.position : Vector3.zero;
    }
}
