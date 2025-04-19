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

    private void Update()
    {
        if (dropSystem.block == null)
            HandleBlockInput();
        else
            dropSystem.Loop();
    }

    private void HandleBlockInput()
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
                        dropSystem.block = gridSystem.SpawnObject(inventoryEntry.Key.prefab);
                    }
                    else
                        Debug.Log("Not enough blocks.");
                }
                else
                    Debug.Log($"No block assigned to key {i}.");
            }
        }
    }

}
