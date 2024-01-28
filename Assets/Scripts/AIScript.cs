using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    public LayerMask movingTargets, whatIsBase;
    public GameObject target;

    private NavMeshAgent agent;

    public int health;
    public int damage;
    public float speed;

    public bool inSightRange;
    public float sightRange;
    public float attackRange = 0.5f;

    public float timeBetweenAttacks;

    public bool targetInSightRange, targetInAttackRange, baseInAttackRange, alreadyAttacked;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        agent.SetDestination(target.transform.position);

    }

    // Update is called once per frame
    void Update()
    {

        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, movingTargets);
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, movingTargets);
        baseInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsBase);

        if (baseInAttackRange) AttackBase();
        if (!targetInSightRange && !targetInAttackRange && !baseInAttackRange) GoToBase();
        if (targetInSightRange && !targetInAttackRange && !baseInAttackRange) Chase();
        if (targetInSightRange && targetInAttackRange && !baseInAttackRange) Attack();
    }



    private void GoToBase()
    {
        agent.SetDestination(target.transform.position);




    }

    private void AttackBase()
    {

        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            Collider[] hitBase = Physics.OverlapSphere(transform.position, attackRange, whatIsBase);

            foreach (Collider enemy in hitBase)
            {
                enemy.GetComponent<BaseHealthScript>().TakeDamage(damage);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    public void Chase()
    {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, sightRange, movingTargets);

        if (hits.Length > 0)
        {
            int randomIndex = Random.Range(0, hits.Length);
            agent.SetDestination(hits[randomIndex].transform.position);
            if (inSightRange == true)
            {
                agent.SetDestination(transform.position);
            }
        }
    }

    public void Attack()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, movingTargets);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<AIScript>().TakeDamage(damage);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
