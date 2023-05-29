using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    public bool isDull = false; // Indicates if the enemy is dull or not
    public float dullDetectionRange = 60f; // Range for detection when enemy is dull
    public float nonDullDetectionRange = 10f; // Range for detection when enemy is not dull
    public float followDistanceMin = 5f; // Minimum distance to follow player
    public float followDistanceMax = 10f; // Maximum distance to follow player
    public float movementSpeed = 6f;
    public float collisionDistance = 1f;
    
    public PlayerManager playerManager;

    private Transform player; // Reference to the player's transform

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        float detectionRange = isDull ? dullDetectionRange : nonDullDetectionRange;

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (isDull)
            {
                // Approach the player and attack
                ApproachAndAttack();
            } else
            {
                // Follow the player at a distance between followDistanceMin and followDistanceMax
                FollowPlayer();
            }
        }
    }

    private void ApproachAndAttack()
    {
        // Implement your approach and attack logic here
        // This method will be called when the enemy is dull

        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        // Check if the enemy has collided with the player
        if (Vector3.Distance(transform.position, player.position) <= collisionDistance)
        {
            // Calculate a random damage value between 10 and 20
            int damage = Random.Range(10, 21);

            // Apply damage to the player
            if (playerManager.currentLight > playerManager.maxLight)
            {

            }



            playerManager.currentHealth -= damage;
            /*
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }*/
        }
    }

   

    private void FollowPlayer()
    {
        // Calculate a position behind the player within the follow distance range
        Vector3 targetPosition = player.position - player.forward * Random.Range(followDistanceMin, followDistanceMax);

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
    }
}
