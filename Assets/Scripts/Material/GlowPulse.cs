using UnityEngine;

public class GlowPulse : MonoBehaviour
{
    public Material glowMaterial;
    public float pulseSpeed = 2f;
    public float minIntensity = 1f;
    public float maxIntensity = 5f;

    private void Update()
    {
        float emission = minIntensity + Mathf.PingPong(Time.time * pulseSpeed, maxIntensity - minIntensity);
        glowMaterial.SetColor("_EmissionColor", Color.green * emission);
    }
}
