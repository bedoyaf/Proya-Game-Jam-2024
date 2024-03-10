using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : EnemyDefault
{
    [SerializeField] float shootingRange = 10f;
    [SerializeField] float shootingCooldown = 3f; // Cooldown time in seconds

    public Rigidbody2D rb;
    public GameObject EnemyBulletPrefab;
    NavMeshAgent agent;
    bool isShooting = false;
    float shootTimer = 0f;

    new void Start()
    {
        base.Start();
        colour = "purple";
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        bool hasObstacle = HasObstaclesInFrontOfEnemy();

        if (!hasObstacle)
        {
            isIdling = false;
        }
        if (isIdling)
        {
            agent.isStopped = true;
        }
        else
        {

            float diroflooking = -transform.position.x + target.position.x;
            FlipSprite(diroflooking);
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            // Debug.Log(hasObstacle);

            if (distanceToTarget > shootingRange || hasObstacle)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                isShooting = false;
            }
            else
            {
                agent.isStopped = true;
                isShooting = true;
            }

            if (isShooting)
            {
                if (shootTimer <= 0f)
                {
                    Shoot();
                    shootTimer = shootingCooldown; // Reset the cooldown timer
                }
                else
                {
                    shootTimer -= Time.deltaTime; // Decrease the cooldown timer
                }
            }
        }
    }

    void Shoot()
    {
        Vector2 shootDirection = (target.position - transform.position).normalized;
        float bulletSpeed = rb.velocity.magnitude;
        Vector3 currentPosition = transform.position;
        currentPosition.z = 0;
        GameObject bullet = Instantiate(EnemyBulletPrefab, currentPosition, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(shootDirection, currentPosition, bulletSpeed);
    }

    void FlipSprite(float horizontal)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (horizontal > 0)
        {
            // Moving left, flip the sprite
            spriteRenderer.flipX = true;
        }
        else if (horizontal < 0)
        {
            // Moving right, restore the sprite to its original orientation
            spriteRenderer.flipX = false;
        }
        // If horizontal is 0, the player is not moving left or right, so don't change the sprite orientation.
    }
}