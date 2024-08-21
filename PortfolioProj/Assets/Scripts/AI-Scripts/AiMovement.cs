using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiMovement : MonoBehaviour
{
    public Transform player;
    public AIScriptableObject aiSO;

    public Transform[] patrolPoints;
    public float wanderIntervals;

    private float timer;
    
    private NavMeshAgent agent;
    private Rigidbody rb;
    private NavMeshSurface surface;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        timer = wanderIntervals;
        aiSO.isChasingPlayer = false;
        SetRandomDestinations();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (aiSO.isChasingPlayer)
        {
            if (!CanSeePlayer())
            {
                Patrol();
                aiSO.isChasingPlayer = false;
            }
            else
            {
                ChasePlayer();
            }
        }
        else 
        {
            Patrol();
            if (CanSeePlayer())
            {
                aiSO.isChasingPlayer = true;
            }
        }
    }
    void Patrol()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomDestinations();
            timer = wanderIntervals;
        }
    }
    private bool CanSeePlayer()
    {
        Vector3 dirToPlayer = player.position - transform.position;
        float angleToDetect = Vector3.Angle(transform.forward, dirToPlayer);

        if (angleToDetect < aiSO.fieldOfView / 2)
        {
            if (Vector3.Distance(transform.position, player.position) <= aiSO.detectRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dirToPlayer.normalized, out hit, aiSO.detectRange))
                {
                    if (hit.transform == player)
                    {
                        return true;
                    }
                    if (hit.distance < 3 && hit.collider.CompareTag("Wall"))
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    private void SetRandomDestinations()
    {
        if (patrolPoints.Length == 0)
        {
           return;
        }

        if (!agent.hasPath)
        {
            int randomIndex = Random.Range(0, patrolPoints.Length);
            Transform destination = patrolPoints[randomIndex];

            agent.SetDestination(destination.position);
        }
    }
    
    // use for later
    private void IsIdling()
    {
        if (rb.velocity.x < 0.1f && rb.velocity.z < 0.1f)
        {
            aiSO.isIdling = true;
        }
        else
        {
            aiSO.isIdling = false;
        }
    }
}
