using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMelee : EnemyDefault
{
    [SerializeField] float attackingRange = 1f;
    [SerializeField] float attackingCooldown = 1f; // Cooldown time in seconds

    public Rigidbody2D rb;
    public GameObject EnemyAttackPrefab;
    NavMeshAgent agent;
    bool isattacking = false;
    float attackTimer = 0f;

    new void Start()
    {
        base.Start();
        colour = "green";
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        float diroflooking = -transform.position.x + target.position.x;
        FlipSprite(diroflooking);
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        bool hasObstacle = HasObstaclesInFrontOfEnemy();
        // Debug.Log(hasObstacle);
        if (distanceToTarget > giveUpLength)
        {
            //Debug.Log(spawnLocation);
            agent.SetDestination(spawnLocation);
        }
        else
        {
            if (isIdling)
            {
                if (hasObstacle)
                {
                    agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.position);
                    isIdling = false;
                    isattacking = true;
                }
            }
            else
            {
                if (distanceToTarget > attackingRange || hasObstacle)
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.position);
                    isIdling = false;
                    isattacking = false;
                }
                else
                {
                    agent.isStopped = true;
                    isattacking = true;
                }
            }

            if (isattacking)
            {
                if (attackTimer <= 0f)
                {
                    Attack();
                    attackTimer = attackingCooldown; // Reset the cooldown timer
                }
                else
                {
                    attackTimer -= Time.deltaTime; // Decrease the cooldown timer
                }
            }
        }
    }

    void Attack()
    {
        Vector2 attackDirection = (target.position - transform.position).normalized;
        float bulletSpeed = rb.velocity.magnitude;
        Vector3 currentPosition = transform.position;
        currentPosition.z = 0;
        GameObject meleeObject = Instantiate(EnemyAttackPrefab, transform.position, Quaternion.identity);
        // Set the direction and speed if your melee object has a script for that
        meleeObject.GetComponent<MeleeController>().SetDirection(attackDirection, currentPosition, attackDirection, bulletSpeed);
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
}
