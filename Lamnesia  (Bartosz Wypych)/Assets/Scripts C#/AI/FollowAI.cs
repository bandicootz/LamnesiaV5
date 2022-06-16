using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    public enum States { Patrol, Follow, Attack }

    public NavMeshAgent agent;
    public Transform target;

    public Transform[] wayPoints;

    public States currentState;

    private int currentWayPoints;

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        
    }

    void Update()
    {
        UpdateStates();
    }

    private void UpdateStates()
    {
        switch (currentState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.Follow:
                Follow();
                break;
            case States.Attack:
                Attack();
                break;
        }
    }

    private void Patrol()
    {
        if(agent.destination != wayPoints[currentWayPoints].position)
        {
            agent.destination = wayPoints[currentWayPoints].position;
        }

        if (HasReached())
        {
            currentWayPoints = (currentWayPoints + 1) % wayPoints.Length;
        }
    }

    private void Follow()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void Attack()
    {

    }

    private bool HasReached()
    {
        return (agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }
}
