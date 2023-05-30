using UnityEngine;

public class LightBoltProjectile
    : MonoBehaviour
{
    public float speed = 10f; // Speed in meters per second

    private void Update()
    {
        // Move the light bolt forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
