using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyDefault
{
    [SerializeField] Transform target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
