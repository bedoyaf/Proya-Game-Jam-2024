using System.Collections;
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
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        bool hasObstacle = HasObstaclesInFrontOfEnemy();
        // Debug.Log(hasObstacle);

        if (distanceToTarget > shootingRange || hasObstacle)
        {
            if(isIdling) {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
            }

            isShooting = false;
        }
        else
        {
            isIdling = false;
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

    void Shoot()
    {
        Vector2 shootDirection = (target.position - transform.position).normalized;
        float bulletSpeed = rb.velocity.magnitude;
        Vector3 currentPosition = transform.position;
        currentPosition.z = 0;
        GameObject bullet = Instantiate(EnemyBulletPrefab, currentPosition, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(shootDirection, currentPosition, bulletSpeed);
    }

}
