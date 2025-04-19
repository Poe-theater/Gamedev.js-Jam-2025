using System;
using System.Collections.Generic;
using UnityEngine;

public class LaneSystem : MonoBehaviour
{
    [SerializeField] private List<Block> unit;
    [SerializeField] private Transform[] leftDestination;
    [SerializeField] private Transform[] rightDestination;


    private void Start()
    {
        LanePoint.OnAnyCollisionEnter += LanePoint_OnAnyCollisionEnter;
    }

    private void OnDisable()
    {
        LanePoint.OnAnyCollisionEnter -= LanePoint_OnAnyCollisionEnter;
    }


    private void LanePoint_OnAnyCollisionEnter((int, bool) tuple, Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Block>(out Block block))
        {
            block.SetUnitMode(leftDestination[tuple.Item1]);
            unit.Add(block);
            Debug.Log("added block to unit");
        }
        else
        {
            Debug.Log("test");
        }
    }
}
