using UnityEngine;
using System.Collections.Generic;
using System;
using NUnit.Framework.Constraints;

public class GridSystem : MonoBehaviour
{

    [SerializeField] private Transform pieceParent;
    [SerializeField] private List<Block> blockSpawned;

    private Rigidbody blockRigidbody;

    public GameObject quadBlocker;
    public event EventHandler OnGridLvUp;
    public event EventHandler OnSpawnObject;

    /// <summary>
    /// Spawns a new object based on the provided prefab. The new block is placed at the quadBlocker's position
    /// with no rotation and parented to pieceParent.
    /// </summary>
    /// <param name="prefab">The transform prefab to spawn.</param>
    /// <returns>The transform of the newly spawned block.</returns>
    public Block SpawnObject(GameObject prefab)
    {
        OnSpawnObject?.Invoke(this, EventArgs.Empty);
        
        Vector3 spawnPos = quadBlocker.transform.position;
        spawnPos.y += 15;
        GameObject newBlock = Instantiate(prefab, spawnPos, Quaternion.identity, pieceParent);
        blockSpawned.Add(newBlock.GetComponent<Block>());
        return blockSpawned[^1];
    }

    public void Update() 
    {
        foreach (Block block in blockSpawned) 
            block.UpdateStatus();
    }
}
