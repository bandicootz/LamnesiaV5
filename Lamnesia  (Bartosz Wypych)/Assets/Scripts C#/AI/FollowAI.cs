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

    [Header("AI Properties")]
    public float maxFollowDistance = 20f;
    public float shootDistance = 10f;
    public Weapon attackWeapon;

    private bool inSight;
    private Vector3 directionToTarget;

    public States currentState;

    private int currentWayPoint = 0;

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
        CheckForPlayer();
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

    private void CheckForPlayer()
    {
        directionToTarget = target.position - transform.position;

        RaycastHit hitInfo;

        if(Physics.Raycast(transform.position, directionToTarget.normalized, out hitInfo))
        {
            inSight = hitInfo.transform.CompareTag("Player");
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

        if (inSight)
        {
            currentState = States.Follow;
        }
    }

    private void Follow()
    {
        if(directionToTarget.magnitude <= shootDistance && inSight)
        {
            agent.ResetPath();
            currentState = States.Attack;
        }

        else
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }

            if (directionToTarget.magnitude > maxFollowDistance)
            {
                currentState = States.Patrol;
            }
        }
    }

    private void Attack()
    {
        if (!inSight || directionToTarget.magnitude > shootDistance)
        {
            currentState = States.Follow;
        }

        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector3 lookDirection = directionToTarget;
        lookDirection.y = 0f;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    private bool HasReached()
    {
        return (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }
}