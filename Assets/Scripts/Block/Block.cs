using System;
using UnityEngine.AI;
using UnityEngine;


public enum BlockState { JUST_SPAWNED = 0, FALLING = 1, PLACED = 2, WALKING = 3, ATTACKING = 4 }

[Serializable]
public class Block : MonoBehaviour
{
    [SerializeField] private Transform transformDestination;
    [SerializeField] private NavMeshAgent agent;
    public BlockState blockState;
    public Rigidbody rb;
    public Animator animator;
    public bool isStarted = false;
    public HandlerBlockAttack handlerBlockAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = -transform.right * 105;
        Gizmos.DrawRay(transform.position, direction);
    }
    void FaceTarget()
    {

        Vector3 direction = (agent.steeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    public bool ReachedDestinationOrGaveUp()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void Update()
    {
        if (!agent.enabled)
            return;

        if (!ReachedDestinationOrGaveUp())
        {

            if (handlerBlockAttack.collideWithEnemy)
            {
                print("luckyy me ");
            }

            FaceTarget();
        }
        else
        {
            OnExplosion();
        }
    }

    bool IsEnemyBlockingAhead()
    {
        print("IsEnemyBlockingAhead");
        Collider[] hits = Physics.OverlapSphere(
        transform.position + transform.forward * 1f,
            0.5f,
            LayerMask.GetMask("Blocks")
        );
        return hits.Length > 0;
    }

    void HandleBlockedByEnemy()
    {
        // e.g., stop, attack, recalculate path, play animation…
        agent.isStopped = true;
        print("HandleBlockedByEnemy");
        //animator.SetTrigger("BlockedAttack");
    }

    private void OnExplosion()
    {
        agent.enabled = false;
        Destroy(gameObject);
    }

    public void SetUnitMode(Transform destination)
    {
        transformDestination = destination;
        blockState = BlockState.WALKING;

        DisableRigidBody();
        animator.enabled = true;
        agent.enabled = true;
        agent.destination = transformDestination.position;
        animator.SetBool("IsWalking", true);
    }
}