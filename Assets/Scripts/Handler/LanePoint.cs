using System;
using UnityEngine;

public class LanePoint : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            if (collision.gameObject.TryGetComponent<Block>(out Block block))
            {
                block.SetUnitMode(destination);
            }
            else
            {
            }
        }
    }
}
