using UnityEngine;
using System;

public enum BlockState { JUST_SPAWNED = 0, FALLING = 1, PLACED = 2}

[Serializable]
public class Block : MonoBehaviour
{
    public BlockState blockState;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void UpdateStatus()
    {
        if (blockState == BlockState.JUST_SPAWNED)
        {
            if (rb.linearVelocity.y > 0)
                blockState = BlockState.FALLING;
            else if (Mathf.Approximately(rb.linearVelocity.y, 0))
                blockState = BlockState.PLACED;
        }
    }

    public void Rotate(bool isLeft)
    {
        if (isLeft)
            transform.Rotate(0, 90, 0);
        else
            transform.Rotate(0, 0, 90);
    }
}
