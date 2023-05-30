using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool isDull = false; // Indicates if the enemy is dull or not
    public float dullDetectionRange = 60f; // Range for detection when enemy is dull
    public float nonDullDetectionRange = 10f; // Range for detection when enemy is not dull
    public float followDistanceMin = 5f; // Minimum distance to follow player
    public float followDistanceMax = 10f; // Maximum distance to follow player
    public float movementSpeed = 6f;
    public float collisionDistance = 1f;
    public int threat = 1;
    public float health = 100;
    
    private PlayerManager playerManager;

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
                playerManager.currentLight = playerManager.maxLight;
            }
            playerManager.currentHealth -= damage;
        }
    }
    private void FollowPlayer()
    {
        // Calculate a position behind the player within the follow distance range
        Vector3 targetPosition = player.position - player.forward * Random.Range(followDistanceMin, followDistanceMax);

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
    }
    public void TakeDamage( float damage)
    {
        health -= damage;
        playerManager.currentXP += 5f;
        if (health <= 0)
        {

            Destroy(gameObject);
            playerManager.currentXP += 30f;
        }
    }
}/*
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 60f;
    public float approachDistance = 5f;

    private bool isDull = true;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isDull && distanceToPlayer <= detectionRange)
        {
            // Approach the player if within detection range
            ApproachPlayer();
        } else if (!isDull && distanceToPlayer > approachDistance)
        {
            // Follow the player at a distance between 5 and 10 meters
            FollowPlayer();
        }
    }

    private void ApproachPlayer()
    {
        // Implement your approach logic here
        // Move towards the player while maintaining a minimum distance of 5 meters
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position - direction * Mathf.Max(approachDistance, Vector3.Distance(transform.position, player.position));
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
    }

    private void FollowPlayer()
    {
        // Implement your follow logic here
        // Move towards the player while maintaining a distance between 5 and 10 meters
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position - direction * Mathf.Clamp(Vector3.Distance(transform.position, player.position), approachDistance, 10f);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
    }
}
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 3f; // Speed of enemy movement
    public float wobbleDuration = 0.5f; // Duration of wobble off course
    public float wobbleMagnitude = 1f; // Magnitude of wobble off course

    private Transform player; // Reference to the player's Transform
    private Vector3 originalDirection; // Original direction towards the player
    private bool isWobbling = false; // Flag to track wobbling state
    private float wobbleTimer = 0f; // Timer for wobbling duration
    private float nextWobbleTime = 0f; // Time for the next wobble

    [SerializeField] private float enemyDamage = 1.0f; //Enemy Damage to Player's Health
    [SerializeField] private PlayerMovement playerMovement; //Reference PlayerMovement Script 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's GameObject by tag
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        originalDirection = (player.position - transform.position).normalized; // Calculate original direction towards the player
        CalculateNextWobbleTime(); // Calculate the time for the next wobble
    }

    private void Update()
    {
        if (isWobbling)
        {
            // Perform wobbling off course
            transform.position += (originalDirection + Random.insideUnitSphere * wobbleMagnitude) * movementSpeed * Time.deltaTime;
            wobbleTimer -= Time.deltaTime;

            if (wobbleTimer <= 0f)
            {
                // Reset back to tracking the player after wobble duration expires
                isWobbling = false;
                CalculateNextWobbleTime(); // Calculate the time for the next wobble
            }
        } else
        {
            // Calculate the direction towards the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Move towards the player
            transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
            transform.LookAt(player.position);
            // Check if it's time for the next wobble
            if (Time.time >= nextWobbleTime)
            {
                // Initiate wobble
                isWobbling = true;
                wobbleTimer = wobbleDuration;
            }
        }
    }

    private void CalculateNextWobbleTime()
    {
        // Generate a random interval for the next wobble
        float randomInterval = Random.Range(1f, 5f);
        nextWobbleTime = Time.time + randomInterval;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            //Damage player health
           // playerMovement.TakeDamage(1);
            Debug.Log("kill");
        }
    }
}
*/