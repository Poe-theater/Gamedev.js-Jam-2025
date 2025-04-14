using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private List<BlockObjectSO> blocks;
    [SerializeField] private List<Transform> blockPlaced;
    [SerializeField] private List<Collider> colliders;
    public GameObject quadBlocker;

    public Transform SpawnObject(int index2)
    {
        int index = UnityEngine.Random.Range(0, blocks.Count);
        Vector3 pos = quadBlocker.transform.position;
        pos.y += 10;
        Transform block = Instantiate(blocks[index].prefab, pos, Quaternion.identity, null);
        blockPlaced.Add(block);
        return block;
    }

    public void ChangeBlockerStatus() =>
        quadBlocker.SetActive(!quadBlocker.activeSelf);

}
