using System;
using UnityEngine;
using UnityEngine.AI;

public enum BlockState { JUST_SPAWNED = 0, FALLING = 1, PLACED = 2, WALKING = 3, ATTACKING = 4}

[Serializable]
public class Block : MonoBehaviour
{
    [SerializeField] private Transform transformDestination;
    [SerializeField] private NavMeshAgent agent;
    public float initialX;
    public BlockState blockState;
    public Rigidbody rb;
    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        initialX = transform.eulerAngles.x;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
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
        if (isLeft && blockState == BlockState.JUST_SPAWNED)
            transform.Rotate(0, 90, 0);
        else
            transform.Rotate(0, 0, 90);
    }

    private void DisableRigidBody()
    {
        rb.detectCollisions = false;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (agent.enabled && transformDestination)
            agent.destination = transformDestination.position;
    }

    public void SetUnitMode(Transform destination)
    {
        blockState = BlockState.WALKING;

        DisableRigidBody();
        animator.enabled = true;
        agent.enabled = true;
        transformDestination = destination;
    }
}
