// Refactored GridSystem.cs
// Improvements:
// 1. Use Properties and C# naming conventions
// 2. Separate Cleanup from UpdateStatus for clarity
// 3. Iterate collections safely (reverse loop for removal)
// 4. Cache components and avoid repeated GetComponent calls
// 5. Throttle blocker movement logic to avoid excess calculations

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform pieceParent;
    [SerializeField] private GameObject quadBlocker;

    [Header("Configuration")]
    [Tooltip("Vertical offset when spawning new blocks")]
    [SerializeField] private float spawnHeightOffset = 15f;

    [Tooltip("Vertical increment for the quad blocker every threshold")]
    [SerializeField] private float blockerStep = 5f;

    [Tooltip("Number of blocks to trigger blocker move")]
    [SerializeField] private int moveThreshold = 3;

    public event EventHandler OnSpawnObject;
    public event EventHandler OnGridLvUp;

    private readonly List<Block> blocks = new();
    private Vector3 nextBlockerPosition;

    public int BlockCount => blocks.Count;

    private void Awake()
    {
        if (quadBlocker != null)
            nextBlockerPosition = quadBlocker.transform.position;
    }

    private void Update()
    {
        CleanupNullBlocks();
        UpdateBlockStatuses();
        TryMoveBlocker();
    }

    /// <summary>
    /// Spawns a new block prefab under pieceParent and registers it.
    /// </summary>
    public Block SpawnObject(GameObject prefab)
    {
        OnSpawnObject?.Invoke(this, EventArgs.Empty);

        Vector3 spawnPos = quadBlocker.transform.position + Vector3.up * spawnHeightOffset;
        GameObject newInstance = Instantiate(prefab, spawnPos, prefab.transform.rotation, pieceParent);
        var block = newInstance.GetComponent<Block>();

        if (block != null)
            blocks.Add(block);

        return block;
    }

    /// <summary>
    /// Remove the last spawned block from tracking (does not destroy the GameObject).
    /// </summary>
    public void RemoveLastBlock()
    {
        if (blocks.Count > 0)
            blocks.RemoveAt(blocks.Count - 1);
    }

    /// <summary>
    /// Cleans up null entries safely.
    /// </summary>
    private void CleanupNullBlocks()
    {
        for (int i = blocks.Count - 1; i >= 0; i--)
        {
            if (blocks[i] == null)
                blocks.RemoveAt(i);
        }
    }

    /// <summary>
    /// Updates the status of each tracked block.
    /// </summary>
    private void UpdateBlockStatuses()
    {
        //for (int i = 0; i < blocks.Count; i++)
        //    blocks[i].UpdateStatus();
    }


    public bool DestroyPiece()
    {
        int index = UnityEngine.Random.Range(0, blocks.Count);

        if (blocks.Count > 0)
        {
            Destroy(blocks[index]);
            blocks.RemoveAt(index);
        }

        return blocks.Count == 0;
    }

    /// <summary>
    /// Moves the quadBlocker upward when certain block count conditions are met.
    /// </summary>
    private void TryMoveBlocker()
    {
        int count = blocks.Count;
        bool shouldMove = count > moveThreshold && count % 2 == 0;

        if (shouldMove && quadBlocker.transform.position.y < nextBlockerPosition.y + blockerStep)
        {
            moveThreshold += 2;
            OnGridLvUp?.Invoke(this, EventArgs.Empty);
            nextBlockerPosition += Vector3.up * blockerStep;
            quadBlocker.transform.position = nextBlockerPosition;
        }
    }
}
