using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyDefault
{
    NavMeshAgent agent;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        colour = "green";
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
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
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }
}
