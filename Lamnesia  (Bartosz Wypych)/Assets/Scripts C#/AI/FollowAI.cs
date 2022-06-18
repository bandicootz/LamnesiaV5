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

    private int currentWayPoint;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        
    }

    private void Update()
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
        if (agent.destination != wayPoints[currentWayPoint].position)
        {
            agent.destination = wayPoints[currentWayPoint].position;
        }

        if (HasReached())
        {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
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
        return (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }
}