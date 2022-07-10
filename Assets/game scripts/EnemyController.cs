using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController: MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
	public Transform enemy;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
	public GameObject deadeffect;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
		 alreadyAttacked = false;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            ///Attack code here
			Vector3 temp = transform.position+new Vector3(0,1.0f,0f);
            Rigidbody rb = Instantiate(projectile,temp, Quaternion.identity).GetComponent<Rigidbody>();
			Vector3 temp2 = transform.forward+new Vector3(0,1.0f,0f);
            rb.AddForce(temp2 *32f, ForceMode.Impulse);
			Vector3 temp3 = transform.up+new Vector3(0,1.0f,0f);
            rb.AddForce(temp3 * 8f, ForceMode.Impulse);
			// StartCoroutine(bulleteffect());
// 			enemy = GameObject.FindGameObjectWithTag("enemy").transform;
// 			  Vector3 temp = enemy.position+new Vector3(0.5f,0,0);
// //   player.position += temp;
//    GameObject _effect=Instantiate(deadeffect,temp,Quaternion.identity);
			
            ///End of attack code

            // alreadyAttacked = true;
            // Invoke(nameof(ResetAttack), timeBetweenAttacks);
			// if(!playerInAttackRange){
			// 	ResetAttack();
            //     //   Invoke(nameof(ResetAttack), timeBetweenAttacks);
			// }
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
// 	IEnumerator bulleteffect(){
//   yield  return new WaitForSeconds(1.5f);
//   Vector3 temp = new Vector3(1.0f,0,0);
//   gun.position += temp;
//    GameObject _effect=Instantiate(deadeffect,gun.position,Quaternion.identity);
//    Destroy(_effect,3f);
//    Destroy(gameObject);
    // }
}
