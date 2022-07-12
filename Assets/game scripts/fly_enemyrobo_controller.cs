using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class fly_enemyrobo_controller: MonoBehaviour {

	public float lookRadius = 10f;	// Detection range for player

	Transform target;	// Reference to the player
	NavMeshAgent agent; // Reference to the NavMeshAgent
    public Transform enemy;
	private AudioSource audioSource;
	// public AudioClip shoot;
	public float playRate = 1;
    private float nextPlayTime = 0;
	public AudioSource attacksource;
	public LayerMask  whatIsPlayer;
	  public float sightRange, attackRange;
	  public bool playerInSightRange, playerInAttackRange;

bool attack;
private GameObject temp_player;
	public ParticleSystem gunflash;

	// Use this for initialization
	private Vector3 temp;
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		enemy=GetComponent<Transform>();
		audioSource=GetComponent<AudioSource>();
		temp= enemy.position;
		audioSource.Play();

	}
	
	// Update is called once per frame
	void Update () {
		// Distance to the target
		temp_player=GameObject.FindGameObjectWithTag("Player");
		if(temp_player!=null){
		target = GameObject.FindGameObjectWithTag("Player").transform;
		}
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
		float distance = Vector3.Distance(target.position, transform.position);
		fly_enemy_health_script health_script=enemy.GetComponent<fly_enemy_health_script>();
		if(health_script.health>0 && target!=null){
		starting();
		if(!playerInSightRange && !playerInAttackRange && !health_script.enemy_hurting){
			Invoke("Patroling",10);
		}
		}
	}
	private void Patroling(){
		if(Vector3.Distance(enemy.position,temp)<=lookRadius){
			starting();
		}
		else if(Vector3.Distance(target.position, transform.position) >lookRadius){
			fly_enemy_health_script health_script=enemy.GetComponent<fly_enemy_health_script>();
			if(health_script.health>0){
                 gunflash.Stop();
			agent.SetDestination(temp);
			}
			  }
	}
	void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
	private void starting(){
		fly_enemy_health_script health_script=enemy.GetComponent<fly_enemy_health_script>();
		if(health_script.health>0){
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance <= lookRadius || health_script.enemy_hurting)
			{
				// Move towards the target
				agent.SetDestination(target.position);
				// If within attacking distance
				if (distance <= agent.stoppingDistance)
				{    
					target_enemy tp=agent.GetComponent<target_enemy>();
					 if ((Time.time > nextPlayTime))
  					  {   
							if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
								gunflash.Play();
								tp.enemy_targeting();
								attacksource.Play();
							nextPlayTime = Time.time + playRate;
							}
							else{
									gunflash.Play();
							tp.enemy_targeting();
							attacksource.Play();
							nextPlayTime = Time.time + playRate;
							}
  						  }
					
					FaceTarget();	// Make sure to face towards the target
				}
			}
		}
	}
	// Show the lookRadius in editor
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
		 Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
	}
}