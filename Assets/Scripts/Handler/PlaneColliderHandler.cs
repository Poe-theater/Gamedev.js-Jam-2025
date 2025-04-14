using UnityEngine;

public class PlaneColliderHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block"))
            Destroy(collision.gameObject);
    }
}
