using UnityEngine;

public class DarkenEdgesController : MonoBehaviour
{
    public Material darkenEdgesMaterial;
    [Range(0f, 1f)]
    public float edgeIntensity = 0.5f;
    [Range(0f, 1f)]
    public float edgeSmoothness = 0.1f;

    private void Update()
    {
        // Update the edge intensity and smoothness values in the shader
        darkenEdgesMaterial.SetFloat("_EdgeIntensity", edgeIntensity);
        darkenEdgesMaterial.SetFloat("_EdgeSmoothness", edgeSmoothness);
    }
}
