using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    //The Player
    [SerializeField] Transform target;

    [SerializeField] float chaseRange = 5.0f;

    //entire scene dimensions 
    float disatanceToTarget = Mathf.Infinity;

    bool isProvoked = false;

    // how close the player can get closae to me
    NavMeshAgent nma;


    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Measure distance between player and enemy
        disatanceToTarget = Vector3.Distance(target.position, transform.position);


            if (isProvoked)
            {
                EngageTarget();
            }
            else if (disatanceToTarget > chaseRange)
            {
                isProvoked = true;

            }
    }

    private void EngageTarget()
    {
        if(disatanceToTarget >= nma.stoppingDistance)
        {
            ChaseTarget();
        }

        if(disatanceToTarget <= nma.stoppingDistance)
        {
            AttackTarget();
        }
    
    }

    private void ChaseTarget()
    {
        nma.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        print(name + " is attcking " + target.name);
    }
        


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

