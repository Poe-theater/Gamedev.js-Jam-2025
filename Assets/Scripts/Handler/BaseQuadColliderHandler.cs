using UnityEngine;

public class BaseQuadColliderHandler : MonoBehaviour
{
    [SerializeField] private Material glowMaterial;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float minIntensity = 1f;
    [SerializeField] private float maxIntensity = 5f;
    [SerializeField] private float emission = 0;
    [SerializeField] private bool haveCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            haveCollided = true;
        }
    }

    private void Update()
    {
        if (haveCollided) 
        {
            emission = minIntensity + Mathf.PingPong(Time.time * pulseSpeed, maxIntensity - minIntensity);
            glowMaterial.SetColor("_EmissionColor", Color.green * emission);

            if (emission > maxIntensity - 1)
            {
                haveCollided = false;
                glowMaterial.SetColor("_EmissionColor", Color.green * minIntensity);
            }
        }
    }
}
