using UnityEngine;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Transform pieceParent;
    [SerializeField] private List<Transform> blockPlaced;
    [SerializeField] private float extraGravityMultiplier = 10.0f;

    public GameObject quadBlocker;

    /// <summary>
    /// Spawns a new object based on the provided prefab. The new block is placed at the quadBlocker's position
    /// with no rotation and parented to pieceParent.
    /// </summary>
    /// <param name="prefab">The transform prefab to spawn.</param>
    /// <returns>The transform of the newly spawned block.</returns>
    public Transform SpawnObject(Transform prefab)
    {
        Transform newBlock = Instantiate(prefab, quadBlocker.transform.position, Quaternion.identity, pieceParent);
        blockPlaced.Add(newBlock);
        ChangeBlockerStatus();
        return newBlock;
    }

    /// <summary>
    /// Applies extra gravitational force to falling blocks (blocks with negative vertical velocity)
    /// in the fixed update loop.
    /// </summary>
    private void FixedUpdate()
    {
        foreach (Transform block in blockPlaced)
        {
            if (block.TryGetComponent<Rigidbody>(out Rigidbody blockRb))
            {
                if (blockRb.linearVelocity.y < 0)
                {
                    float additionalGravityForce = (extraGravityMultiplier - 1f) * Physics.gravity.magnitude;
                    blockRb.AddForce(Vector3.down * additionalGravityForce, ForceMode.Acceleration);
                }
            }
        }
    }

    /// <summary>
    /// Toggles the active status of the quadBlocker.
    /// </summary>
    public void ChangeBlockerStatus() => quadBlocker.SetActive(!quadBlocker.activeSelf);
}
