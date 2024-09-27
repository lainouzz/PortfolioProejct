using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.AI.Navigation;
using Unity.VisualScripting;
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
    bool randomAnim;
    
    private NavMeshAgent agent;
    private Rigidbody rb;
    private NavMeshSurface surface;
    [SerializeField]private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        timer = wanderIntervals;
        aiSO.isChasingPlayer = false;
        SetRandomDestinations();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            
        }
        
        Attacking();
        
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
        //var b = randomAnim != randomAnim;
        agent.SetDestination(player.position);
        
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", true);
         
    }

    void Attacking()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            aiSO.walkSpeed = 0;
            anim.SetBool("isAttacking", true);
            anim.SetBool("isWalking", false);
            Debug.Log("attacking");
        }
        else
        {
            aiSO.walkSpeed = 5;
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", true);
        }
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
            agent.speed = aiSO.walkSpeed;
        }
    }
    
}
