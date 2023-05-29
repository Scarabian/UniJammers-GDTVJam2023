using UnityEngine;

public class GlowEffectController : MonoBehaviour
{
    public Renderer playerRenderer;
    public Color glowColor = Color.white;
    [Range(0f, 1f)]
    public float glowThreshold = 0.5f;

    private Material playerMaterial;
    private Camera mainCamera;

    private void Start()
    {
        // Get the player material and store a reference to the main camera
        playerMaterial = playerRenderer.material;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Cast a ray from the camera to the player
        Ray ray = new Ray(mainCamera.transform.position, playerRenderer.transform.position - mainCamera.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
            // Check if the ray hits any objects between the camera and the player
            if (hit.collider.gameObject != playerRenderer.gameObject)
            {
                Debug.Log("Glow on");
                // Enable the glow effect
                playerMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                playerMaterial.SetColor("_GlowColor", glowColor);
                playerMaterial.SetFloat("_GlowThreshold", glowThreshold);
            } else
            {
                // Disable the glow effect
                playerMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                Debug.Log("Glow off");
            }
        }
    }
}
