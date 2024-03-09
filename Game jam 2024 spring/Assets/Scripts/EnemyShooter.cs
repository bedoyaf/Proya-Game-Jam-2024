using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float shootingRange = 10f;

    public Rigidbody2D rb;
    public GameObject EnemyBulletPrefab;
    NavMeshAgent agent;
    bool isShooting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        bool hasObstacle = HasObstaclesInFrontOfEnemy();
        Debug.Log(hasObstacle);

        if (distanceToTarget > shootingRange || HasObstaclesInFrontOfEnemy())
        {
            //Debug.Log("CantSee");
            agent.isStopped = false;
            agent.SetDestination(target.position);
            isShooting = false;
        }
        else
        {
            //Debug.Log("See you");
            //RotateTowardsTarget();
            agent.isStopped = true;

            isShooting = true;
        }

        if (isShooting)
        {

            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 shootDirection = (target.position - transform.position).normalized;
        float bulletSpeed = rb.velocity.magnitude;
        Debug.Log(bulletSpeed);
        Vector3 currentPosition = transform.position;
        currentPosition.z = 0;
        GameObject bullet = Instantiate(EnemyBulletPrefab, currentPosition, Quaternion.identity);

        bullet.GetComponent<EnemyBullet>().SetDirection(shootDirection, currentPosition, bulletSpeed);
    }

    bool HasObstaclesInFrontOfEnemy()
    {

        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 currentPosition = transform.position;
        currentPosition.z = 0;
        direction.z = 0;
        LayerMask layerMask = LayerMask.GetMask("Player", "Walls");
        RaycastHit2D hit2D = Physics2D.Raycast(currentPosition, direction, 100, layerMask);

        if (hit2D.collider != null)
        {

            if (!hit2D.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}