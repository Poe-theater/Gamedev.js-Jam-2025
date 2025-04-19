using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseSystem : MonoBehaviour
{
    [SerializeField] private List<Block> unit;

    public void SetBlockToUnit(GameObject newBlock)
    {
        if (newBlock.TryGetComponent<Block>(out Block block))
        {
            block.SetUnitMode();
            unit.Add(block);
            Debug.Log("added block to unit");
        }
        else
        {
            Debug.Log("test");
        }
    }
}
