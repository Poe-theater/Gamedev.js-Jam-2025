using UnityEngine;

public class bullet : MonoBehaviour
{
    public bool isLaunched = false;
    public Vector3 dir;
    public Rigidbody rb;

    private void Update()
    {
        if (isLaunched)
        {
            rb.AddForce(dir, ForceMode.Impulse);
        }
    }

}
