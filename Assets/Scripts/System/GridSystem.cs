using UnityEngine;
using System.Collections.Generic;
using System;

public class GridSystem : MonoBehaviour
{

    [SerializeField] private Transform pieceParent;
    [SerializeField] private List<Transform> blockPlaced;
    public event EventHandler OnGridLvUp;
    public GameObject quadBlocker;
    public bool test = false;

    /// <summary>
    /// Spawns a new object based on the provided prefab. The new block is placed at the quadBlocker's position
    /// with no rotation and parented to pieceParent.
    /// </summary>
    /// <param name="prefab">The transform prefab to spawn.</param>
    /// <returns>The transform of the newly spawned block.</returns>
    public Transform SpawnObject(Transform prefab)
    {
        Vector3 spawnPos = quadBlocker.transform.position;
        spawnPos.y += 15;
        Transform newBlock = Instantiate(prefab, spawnPos, Quaternion.identity, pieceParent);
        blockPlaced.Add(newBlock);
        ChangeBlockerStatus();
        return newBlock;
    }
    private void Update()
    {
        for (int i = 0; i < blockPlaced.Count; i++)
        {
            if (blockPlaced[i] == null)
                blockPlaced.Remove(blockPlaced[i]);
        }

        if (blockPlaced.Count % 2 == 0 && !test)
        {
            Vector3 newPos = quadBlocker.transform.position;
            newPos.y += 25;
            quadBlocker.transform.position = newPos;
            test = true;

            OnGridLvUp?.Invoke(this, EventArgs.Empty);
        }
        if (blockPlaced.Count % 2 == 1)
        {
            test = false;
        }
    }
    /// <summary>
    /// Toggles the active status of the quadBlocker.
    /// </summary>
    public void ChangeBlockerStatus() => quadBlocker.SetActive(!quadBlocker.activeSelf);
}
