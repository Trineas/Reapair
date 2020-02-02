using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    public Animator anim;

    public enum AIState
    {
        isIdle,
        isPatroling,
        isChasing,
        isAttacking,
    };

    public AIState currentState;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;
    public float attackRange = 1f;

    public float timeBetweenAttacks = 2f;
    private float attackCounter;

    public int idleSound, walkSound, attackSound;
    public bool idleOn, walkOn, attackOn;

    void Start()
    {
        waitCounter = waitAtPoint;
        walkOn = false;
        idleOn = false;
        attackOn = false;
    }

    void Update()
    {
        if (!walkOn && anim.GetBool("IsMoving"))
        {
            AudioManager.instance.PlaySFX(walkSound);
            walkOn = true;
        }

        if (walkOn && !anim.GetBool("IsMoving"))
        {
            AudioManager.instance.sfx[1].Stop();
            walkOn = false;
        }

        if (!idleOn && !anim.GetBool("IsMoving"))
        {
            AudioManager.instance.PlaySFX(walkSound);
            walkOn = true;
        }

        if (idleOn && anim.GetBool("IsMoving"))
        {
            AudioManager.instance.sfx[2].Stop();
            walkOn = false;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch(currentState)
        {
        case AIState.isIdle:
            anim.SetBool("IsMoving", false);

            if (waitCounter > 0)
            {
                waitCounter -= Time.deltaTime;
            }

            else
            {
                currentState = AIState.isPatroling;
                agent.SetDestination(patrolPoints[currentPatrolPoint].position);
            }

            if (distanceToPlayer <= chaseRange)
            {
                currentState = AIState.isChasing;
                anim.SetBool("IsMoving", true);
            }

        break;

        case AIState.isPatroling:

        if (agent.remainingDistance <= 0.2f)
        {
            currentPatrolPoint++;
            if (currentPatrolPoint >= patrolPoints.Length)
            {
                currentPatrolPoint = 0;
            }

            currentState = AIState.isIdle;
            waitCounter = waitAtPoint;
        }

            if (distanceToPlayer <= chaseRange)
            {
                currentState = AIState.isChasing;
            }

        anim.SetBool("IsMoving", true);

        break;

        case AIState.isChasing:

            agent.SetDestination(PlayerController.instance.transform.position);
            if (distanceToPlayer <= attackRange)
            {
                currentState = AIState.isAttacking;
                anim.SetTrigger("Attack");
                anim.SetBool("IsMoving", false);
                
                agent.velocity = Vector3.zero;
                agent.isStopped = true;

                attackCounter = timeBetweenAttacks;
            }

            if (distanceToPlayer > chaseRange)
            {
                currentState = AIState.isIdle;
                waitCounter = waitAtPoint;

                agent.velocity = Vector3.zero;
                agent.SetDestination(transform.position);
            }

        break;

        case AIState.isAttacking:

            transform.LookAt(PlayerController.instance.transform, Vector3.up);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                if (distanceToPlayer < attackRange)
                {
                    anim.SetTrigger("Attack");

                    if (!attackOn)
                    {
                    AudioManager.instance.PlaySFX(walkSound);
                    attackOn = true;
                    }

                    if (attackOn)
                    {
                    AudioManager.instance.sfx[2].Stop();
                    attackOn = false;
                    }

                    attackCounter = timeBetweenAttacks;
                }

                else
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;

                    agent.isStopped = false;
                }
            }

        break;

        }
    }
}
