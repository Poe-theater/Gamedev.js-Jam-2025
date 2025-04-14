using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject quadBlocker;
    [SerializeField] private float forceMultiplier = 5f;
    [SerializeField] private float extraGravityMultiplier = 2.0f;

    private Vector3 offset;
    private float yCoord;
    private bool isDragging = false;
    private Rigidbody rb;

    private Vector3 previousDragPos;
    private Vector3 dragVelocity;

    void Start()
    {
        yCoord = block.transform.position.y;
        if (!block.TryGetComponent<Rigidbody>(out rb))
        {
            Debug.LogError("Block requires a Rigidbody component.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnClick();
        if (Input.GetMouseButton(0) && isDragging) OnDrag();
        if (Input.GetMouseButtonUp(0)) OnRelease();
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.y < 0)
        {
            float additionalGravity = (extraGravityMultiplier - 1f) * Physics.gravity.magnitude;
            rb.AddForce(Vector3.down * additionalGravity, ForceMode.Acceleration);
        }
    }
    private void SpawnObject()
    {

    }

    private void OnClick()
    {
        offset = block.transform.position - GetMouseWorldPos();
        previousDragPos = block.transform.position;
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
        quadBlocker.SetActive(false);
        isDragging = false;

        rb.AddForce(dragVelocity * forceMultiplier, ForceMode.VelocityChange);
    }

    private Vector3 GetMouseWorldPos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane dragPlane = new Plane(Vector3.up, new Vector3(0, yCoord, 0));

        if (dragPlane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return block.transform.position;
    }
}
