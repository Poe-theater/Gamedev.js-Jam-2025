using System;
using UnityEngine;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour
{

    [SerializeField] private Transform pieceParent;
    [SerializeField] private List<Block> blockSpawned;
    private Rigidbody blockRigidbody;
    public GameObject quadBlocker;
    public event EventHandler OnSpawnObject;

    public Block SpawnObject(GameObject prefab)
    {
        OnSpawnObject?.Invoke(this, EventArgs.Empty);

        Vector3 spawnPos = quadBlocker.transform.position;
        spawnPos.y += 15;
        GameObject newBlock = Instantiate(prefab, spawnPos, prefab.transform.rotation, pieceParent);
        blockSpawned.Add(newBlock.GetComponent<Block>());
        return blockSpawned[^1];
    }

    public void RemoveLastBlock()
    {
        if (blockSpawned.Count > 0)
            blockSpawned.RemoveAt(blockSpawned.Count - 1);
    }

    public void Update() 
    {
        foreach (Block block in blockSpawned) 
            block.UpdateStatus();
    }

    public int getBlockCount()
    { return blockSpawned.Count; }
}
