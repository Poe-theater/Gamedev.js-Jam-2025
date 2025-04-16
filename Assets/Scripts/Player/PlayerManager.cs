using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private PlayerDropSystem dropSystem;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BlockListSO blockListSO;
    [SerializeField] private Dictionary<BlockObjectSO, int> blockInventory = new();

    private void Start()
    {
        InitializeBlockInventory();
        dropSystem.OnBlockDrop += HandleBlockDrop;
    }

    /// <summary>
    /// Initialize the inventory dictionary with all block types from blockListSO set to zero.
    /// </summary>
    private void InitializeBlockInventory()
    {
        foreach (BlockObjectSO blockSO in blockListSO.blockListSO)
            blockInventory.Add(blockSO, 0);
    }

    /// <summary>
    /// Adds one unit of the specified block to the inventory and updates the UI.
    /// </summary>
    public void AddToInventory(BlockObjectSO blockSO)
    {
        if (blockInventory.ContainsKey(blockSO))
        {
            blockInventory[blockSO]++;
            uiManager.UpdateVisual(blockSO.id, blockInventory[blockSO]);
            Debug.Log(blockSO.name + blockInventory[blockSO]);
        }
    }

    /// <summary>
    /// Removes one unit of the specified block from the inventory and updates the UI.
    /// </summary>
    private void RemoveFromInventory(BlockObjectSO blockSO)
    {
        if (blockInventory.ContainsKey(blockSO))
        {
            blockInventory[blockSO]--;
            uiManager.UpdateVisual(blockSO.id, blockInventory[blockSO]);
        }
    }

    private void OnDisable()
    {
        dropSystem.OnBlockDrop -= HandleBlockDrop;
    }

    /// <summary>
    /// Callback for when a block is dropped.
    /// </summary>
    private void HandleBlockDrop(object sender, EventArgs e)
    {
        gridSystem.ChangeBlockerStatus();
    }

    private void Update()
    {
        if (dropSystem.block == null)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    if (blockInventory.Count >= i)
                    {
                        var inventoryEntry = blockInventory.ElementAt(i - 1);
                        if (inventoryEntry.Value > 0)
                        {
                            RemoveFromInventory(inventoryEntry.Key);
                            dropSystem.SetBlock(gridSystem.SpawnObject(inventoryEntry.Key.prefab));
                        }
                        else
                            Debug.Log("Not enough blocks.");
                    }
                    else
                        Debug.Log($"No block assigned to key {i}.");
                }
            }
        }
        else
            dropSystem.Loop();
    }
}
