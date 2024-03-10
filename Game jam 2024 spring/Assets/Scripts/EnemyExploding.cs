using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class EnemyExploding : EnemyDefault
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] float bombPlantingDistance = 2f;
    [SerializeField] float safeDistance = 10f; // Distance to run away from player and bomb
    [SerializeField] float explosionCooldown = 2.5f; // Cooldown between bomb planting

    NavMeshAgent agent;
    Vector3 lastBombPosition;
    bool hasPlantedBomb = false;
    float explosionTimer = 0f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        colour = "red";
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(explosionTimer);
        if (!HasObstaclesInFrontOfEnemy())
        {
            isIdling = false;
        }
        if (isIdling)
        {
            agent.isStopped = true;
        }
        else
        {
            // If bomb cooldown is over, reset the cooldown and plant a bomb
            if (explosionTimer <= 0f)
            {
                hasPlantedBomb = false; // Reset the bomb planting flag
                explosionTimer = 0; // Reset the cooldown timer
            }
            else
            {
                explosionTimer -= Time.deltaTime; // Decrease the cooldown timer
            }

            // If bomb is not planted, approach the player
            if (!hasPlantedBomb)
            {
                if (!HasObstaclesInFrontOfEnemy())
                {
                    isIdling = false;
                }
                if (isIdling)
                {
                    agent.isStopped = true;
                }
                else
                {
                 //   Debug.Log("KILL");
                    agent.isStopped = false;
                    agent.SetDestination(target.position);
                }

                // Check if the enemy is close enough to the player to plant the bomb
                if (Vector3.Distance(transform.position, target.position) <= bombPlantingDistance)
                {
                    hasPlantedBomb = true;
                    explosionTimer = explosionCooldown;
                    PlantBomb();
                }
            }
            // If bomb is already planted, run away
            else
            {
               // Debug.Log("Run");
                Vector3 directionToPlayer = (transform.position - target.position).normalized;
                Vector3 directionToBomb = (transform.position - lastBombPosition).normalized;
                Vector3 safeDirection = (directionToPlayer + directionToBomb).normalized;

                Vector3 targetPosition = transform.position + safeDirection * safeDistance;
                agent.SetDestination(targetPosition);
                Debug.Log(targetPosition);
            }
            float diroflooking = -transform.position.x + target.position.x;
            FlipSprite(diroflooking);
        }
    }
    void FlipSprite(float horizontal)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (horizontal < 0)
        {
            // Moving left, flip the sprite
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            // Moving right, restore the sprite to its original orientation
            spriteRenderer.flipX = false;
        }
        // If horizontal is 0, the player is not moving left or right, so don't change the sprite orientation.
    }
    // Plant the bomb at a position adjacent to the player
    void PlantBomb()
    {
        Vector3 lastBombPosition = GetBombPosition();
        Instantiate(bombPrefab, lastBombPosition, Quaternion.identity);
    }

    // Calculate the position to plant the bomb (adjacent to the player)
    Vector3 GetBombPosition()
    {
        Vector3 directionToPlayer = (target.position - transform.position).normalized;
        Vector3 bombPosition = target.position + directionToPlayer * -bombPlantingDistance;
        return bombPosition;
    }
}
