using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private PlayerDropSystem dropSystem;

    [SerializeField] private BlockListSO blockListSO;
    private Dictionary<BlockObjectSO, int> blockInventory = new Dictionary<BlockObjectSO, int>();

    private void Start()
    {
        dropSystem.yCoord = gridSystem.quadBlocker.transform.position.y + 15;
        CreateBlock();
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
            Debug.Log(blockSO.name + blockInventory[blockSO]);
        }
    }

    [ContextMenu("create block")]
    void CreateBlock()
    {
        dropSystem.SetBlock(gridSystem.SpawnObject(0));
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
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            CreateBlock();
        }
        dropSystem.Loop();
    }

}
