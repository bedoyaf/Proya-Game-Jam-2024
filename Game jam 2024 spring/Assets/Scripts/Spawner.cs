using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : EnemyDefault
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    private float timer;

    void Start()
    {
        base.Start();
        UpdateHealthBar();
        timer = spawnInterval;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= giveUpLength)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                SpawnEnemy();
                timer = spawnInterval;
            }
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
