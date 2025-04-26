using System;
using UnityEngine.AI;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


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
    public BoxCollider[] boxColliders;
    public bool isAttacking = false;
    public GameObject enemy = null;
    public float maxSpeed = 0;
    private void Awake()
    {
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.updatePosition = false;
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
        rb.useGravity = true;
        rb.isKinematic = false;
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

    private void FixedUpdate()
    {
        if (isStarted)
        {
            Vector3 direction = (agent.nextPosition - transform.position).normalized;
            rb.linearVelocity = direction * agent.speed;
            FaceTarget();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (!enemy && other.transform.CompareTag("Block"))
        {
            enemy = other.gameObject;
            Debug.Log($"OnTriggerEnter {name} touched {other.transform.name}");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enemy && collision.transform.CompareTag("Block"))
        {
            enemy = collision.gameObject;
            Debug.Log($"OnTriggerEnter {name} touched {collision.transform.name}");
        }
    }

    private void Update()
    {
        if (!agent.enabled)
            return;


        if (!ReachedDestinationOrGaveUp())
        {
            //if (agent.velocity.magnitude > maxSpeed)
            //    maxSpeed = agent.velocity.magnitude;

            //if (!isStarted && agent.velocity.magnitude > 1)
            //    isStarted = true;

            //if (isStarted && (agent.velocity.magnitude)  < maxSpeed - 2)
            //{
            //    animator.SetTrigger("Attack");
            //    isAttacking = true;
            //}

            FaceTarget();
        }
        else
        {
            OnExplosion();
        }
    }

    //bool IsEnemyBlockingAhead()
    //{
    //    print("IsEnemyBlockingAhead");
    //    Collider[] hits = Physics.OverlapSphere(
    //    transform.position + transform.forward * 1f,
    //        0.5f,
    //        LayerMask.GetMask("Blocks")
    //    );
    //    return hits.Length > 0;
    //}

    //void HandleBlockedByEnemy()
    //{
    //    // e.g., stop, attack, recalculate path, play animation…
    //    agent.isStopped = true;
    //    print("HandleBlockedByEnemy");
    //    //animator.SetTrigger("BlockedAttack");
    //}

    private void OnExplosion()
    {
        //agent.enabled = false;
        //Destroy(gameObject);
    }

    public void SetUnitMode(Transform destination)
    {
        foreach (BoxCollider box in boxColliders)
            box.enabled = true;

        transformDestination = destination;
        blockState = BlockState.WALKING;
        isStarted = true;

        DisableRigidBody();
        animator.enabled = true;
        agent.enabled = true;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.updatePosition = false;
        agent.destination = transformDestination.position;
        animator.SetBool("IsWalking", true);
    }
}