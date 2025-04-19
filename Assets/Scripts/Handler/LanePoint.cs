using System;
using UnityEngine;

public class LanePoint : MonoBehaviour
{
    public static event Action<GameObject, Collision> OnAnyCollisionEnter;
    [SerializeField] private Transform blockSnapPos;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            Vector3 newPos = collision.gameObject.transform.position;
            newPos.y = blockSnapPos.position.y;
            newPos.z = blockSnapPos.position.z;
            collision.gameObject.transform.position = newPos;
            OnAnyCollisionEnter?.Invoke(gameObject, collision);
        }
    }
}
