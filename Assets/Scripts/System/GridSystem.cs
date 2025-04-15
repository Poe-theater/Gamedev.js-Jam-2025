using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> blockPlaced;
    [SerializeField] private List<Collider> colliders;
    public GameObject quadBlocker;

    public Transform SpawnObject(Transform prefab)
    {
        blockPlaced.Add(
            Instantiate(prefab, quadBlocker.transform.position, Quaternion.identity, null)
            );
        ChangeBlockerStatus();
        return blockPlaced[^1];
    }

    public void ChangeBlockerStatus() =>
        quadBlocker.SetActive(!quadBlocker.activeSelf);

}
