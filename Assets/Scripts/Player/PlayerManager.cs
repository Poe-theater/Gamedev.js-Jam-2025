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
        dropSystem.yCoord = gridSystem.quadBlocker.transform.position.y + 15;
        InitializeBlockInventory();
        dropSystem.OnBlockDrop += DropSystem_OnBlockDrop;
    }

    private void InitializeBlockInventory()
    {
        foreach (BlockObjectSO blockSO in blockListSO.blockListSO)
        {
            blockInventory.Add(blockSO, 0);
        }
    }

    public void AddToInventory(BlockObjectSO blockSO)
    {
        if (blockInventory.ContainsKey(blockSO))
        {
            blockInventory[blockSO] += 1;
            uiManager.UpdateVisual(blockSO.GetInstanceID(), blockInventory[blockSO]);
            Debug.Log(blockSO.name + blockInventory[blockSO]);
        }
    }

    private void RemoveFromInventory(BlockObjectSO blockSO)
    {
        if (blockInventory.ContainsKey(blockSO))
        {
            blockInventory[blockSO] -= 1;
            uiManager.UpdateVisual(blockSO.GetInstanceID(), blockInventory[blockSO]);
        }
    }

    private void OnDisable()
    {
        dropSystem.OnBlockDrop -= DropSystem_OnBlockDrop;
    }

    private void DropSystem_OnBlockDrop(object sender, System.EventArgs e)
    {
        gridSystem.ChangeBlockerStatus();
    }

    private void Update()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                if (blockInventory.Count >= i)
                {
                    var entry = blockInventory.ElementAt(i - 1);
                    if (entry.Value > 0)
                    {
                        RemoveFromInventory(entry.Key);
                        dropSystem.SetBlock(gridSystem.SpawnObject(entry.Key.prefab));
                    }
                    else
                        Debug.Log("Not enough blocks.");
                }
                else
                    Debug.Log($"No block assigned to key {i}.");
            }
        }
        dropSystem.Loop();
    }

}
