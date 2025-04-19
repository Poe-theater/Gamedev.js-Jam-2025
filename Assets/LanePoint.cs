using UnityEngine;

public class LanePoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block")) 
        {
            collision.gameObject.transform.position = transform.position;
        }
    }
}
