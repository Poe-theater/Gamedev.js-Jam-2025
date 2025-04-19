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
            //Vector3 newPos = collision.gameObject.transform.position;
            //newPos.y = blockSnapPos.position.y;
            //newPos.z = blockSnapPos.position.z;
            //collision.gameObject.transform.position = newPos;
            OnAnyCollisionEnter?.Invoke((pointIndex, isLeft), collision);
        }
    }
}
