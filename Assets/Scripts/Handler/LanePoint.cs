using System;
using UnityEngine;

public class LanePoint : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    [SerializeField] private int pointIndex;
    [SerializeField] private Transform blockSnapPos;

    public static event Action<(int, bool), Collision> OnAnyCollisionEnter;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            collision.transform.rotation = transform.rotation;
            OnAnyCollisionEnter?.Invoke((pointIndex, isLeft), collision);
        }
    }
}
