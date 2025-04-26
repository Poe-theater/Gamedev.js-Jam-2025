using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private PlayerDropSystem dropSystem;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BlockListSO blockListSO;
    [SerializeField] private Dictionary<BlockObjectSO, int> blockInventory = new();

    public GameObject[] projectile;

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

    private void ClearInventory()
    {
        List<BlockObjectSO> keys = new();

        for (int i = 0; i < keys.Count; i++)
        {
            BlockObjectSO key = keys[i];
            blockInventory[keys[i]] = 0;
            uiManager.UpdateVisual(keys[i].id, blockInventory[keys[i]]);
        }
    }

    private void Update()
    {
        if (dropSystem.block == null)
            HandleBlockInput();
        else
            dropSystem.Loop();

        if (Input.GetKeyDown(KeyCode.C)) LaunchProjectile(true);
        if (Input.GetKeyDown(KeyCode.V)) LaunchProjectile(false);

    }

    private void LaunchProjectile(bool isLeft)
    {
        ClearInventory();

        if (isLeft)
        {
            projectile[0].GetComponent<bullet>().isLaunched = true;
        }
        else
        {
            projectile[1].GetComponent<bullet>().isLaunched = true;
        }
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
