using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDropSystem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float forceMultiplier = 5f;
    [SerializeField] private float extraGravityMultiplier = 2.0f;

    private Vector3 offset;
    private bool isDragging = false;
    private Rigidbody rb = null;

    private Vector3 previousDragPos;
    private Vector3 dragVelocity;

    public float yCoord;
    public Transform block;

    public event EventHandler OnBlockDrop;

    public void Loop()
    {
        if (block != null)
        {
            if (Input.GetMouseButtonDown(0)) OnClick();
            if (Input.GetMouseButton(0) && isDragging) OnDrag();
            if (Input.GetMouseButtonUp(0)) OnRelease();
            if (Input.GetKeyDown(KeyCode.Q)) Rotate(1);
            if (Input.GetKeyDown(KeyCode.E)) Rotate(-1);
        }
    }

    void Rotate(int dir)
    {
        if (dir == 1)
            block.transform.Rotate(0, 90, 0);
        else
            block.transform.Rotate(0, 0, 90);
    }

    void FixedUpdate()
    {
        if (rb != null && rb.linearVelocity.y < 0)
        {
            float additionalGravity = (extraGravityMultiplier - 1f) * Physics.gravity.magnitude;
            rb.AddForce(Vector3.down * additionalGravity, ForceMode.Acceleration);
        }
    }

    public void SetBlock(Transform blockTransform)
    {
        block = blockTransform;
        blockTransform.TryGetComponent<Rigidbody>(out rb);
    }

    private void OnClick()
    {
        offset = block.position - GetMouseWorldPos();
        previousDragPos = block.position;
        isDragging = true;
    }

    private void OnDrag()
    {
        Vector3 currentDragPos = GetMouseWorldPos() + offset;
        currentDragPos.y = yCoord;
        rb.MovePosition(currentDragPos);

        dragVelocity = (currentDragPos - previousDragPos) / Time.deltaTime;
        previousDragPos = currentDragPos;
    }

    private void OnRelease()
    {
        OnBlockDrop?.Invoke(this, EventArgs.Empty);

        isDragging = false;
        block = null;
        rb.AddForce(dragVelocity * forceMultiplier, ForceMode.VelocityChange);
    }

    private Vector3 GetMouseWorldPos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane dragPlane = new(Vector3.up, new Vector3(0, yCoord, 0));

        if (dragPlane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return block.position;
    }
}
