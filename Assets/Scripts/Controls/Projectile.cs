using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxRange = 30f;

    private Vector3 initialPosition;
    private float traveledDistance;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Calculate the distance traveled
        traveledDistance = Vector3.Distance(initialPosition, transform.position);

        // Check if the traveled distance exceeds the maximum range
        if (traveledDistance >= maxRange)
        {
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}