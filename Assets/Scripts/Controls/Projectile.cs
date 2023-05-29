/*using UnityEngine;

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
}*/

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 30f;
    public int minDamage = 20;
    public int maxDamage = 30;

    public EnemyManager enemyManager;

    private Rigidbody rb;
    private float initialDistance;

    private void Start()
    {
       
        enemyManager = FindObjectOfType<EnemyManager>();
        rb = GetComponent<Rigidbody>();
        initialDistance = 0f;
    }

    private void Update()
    {
        MoveForward();
        
        // Check if the projectile has traveled the maximum distance
        float traveledDistance = Vector3.Distance(transform.position, rb.position);
        Debug.Log(traveledDistance);
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void MoveForward()
    {
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Deal damage to the enemy and destroy the projectile
            
            if (enemyManager != null)
            {
                int damage = Random.Range(minDamage, maxDamage + 1);
                enemyManager.TakeDamage(damage);
            }

            Destroy(gameObject);
        } else
        {
            // Destroy the projectile if it hits anything other than an enemy
            Destroy(gameObject);
        }
    }
}
