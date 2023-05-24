using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 300f; // Speed of rotation in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the GameObject around its up axis (Y-axis in Unity) at the given speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
