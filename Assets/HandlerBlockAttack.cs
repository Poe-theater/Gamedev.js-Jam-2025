using UnityEngine;
using UnityEngine.AI;

public class HandlerBlockAttack : MonoBehaviour
{
    public bool collideWithEnemy = false;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("OnCollisionEnter 15 ");

    //    NavMeshAgent otherAgent = collision.transform.GetComponent<NavMeshAgent>();
    //    if (otherAgent != null && otherAgent != GetComponent<NavMeshAgent>())
    //    {
    //        Debug.Log($"OnCollisionEnter {name} touched {collision.transform.name}");
    //        // Implement your custom logic here
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log($"OnTriggerEnter {name} touched {other.transform.name}");

    //    NavMeshAgent otherAgent = other.transform.GetComponent<NavMeshAgent>();
    //    if (otherAgent != null && otherAgent != GetComponent<NavMeshAgent>())
    //    {
    //        Debug.Log($"OnTriggerEnter {name} touched {other.transform.name}");
    //        // Implement your custom logic here
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.CompareTag("Block"))
    //    {
    //        print($" OnTriggerStay {other.gameObject.name}");
    //        collideWithEnemy = false;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.CompareTag("Block"))
    //    {
    //        print($" OnTriggerExit {other.gameObject.name}");
    //        collideWithEnemy = false;
    //    }
    //}
}
