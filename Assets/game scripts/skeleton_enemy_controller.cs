using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class skeleton_enemy_controller: MonoBehaviour {
	public float lookRadius = 10f;	// Detection range for player

	Transform target;	// Reference to the player
	NavMeshAgent agent; // Reference to the NavMeshAgent
	// public GameObject enemy_t;
    public Transform enemy;
    private Rigidbody rbd;
	private AudioSource audioSource;
	public AudioClip attack_sound;
	public AudioClip moan_sound;
	public float playRate = 1;
    private float nextPlayTime = 0;
	CharacterCombat combat;
	public LayerMask  whatIsPlayer;
	  public float sightRange, attackRange;
	  public bool playerInSightRange, playerInAttackRange;

bool attack;
private GameObject temp_player;
	public Animator anim;
    public AudioSource attack_ads;

	// Use this for initialization
	public Vector3 temp;
	bool shoot_temp;
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		combat = GetComponent<CharacterCombat>();
		enemy=GetComponent<Transform>();
		anim = GetComponent<Animator>();
		audioSource=GetComponent<AudioSource>();
		temp= enemy.position;
		attack_sound=Resources.Load<AudioClip> ("zombie_short_attack2");
		moan_sound=Resources.Load<AudioClip> ("zombie_moan_2");
	}
	
	// Update is called once per frame
	void Update () {
		// Distance to the target
		temp_player=GameObject.FindGameObjectWithTag("Player");
		if(temp_player!=null){
		target = GameObject.FindGameObjectWithTag("Player").transform;
		}
        rbd=enemy.GetComponent<Rigidbody>();
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
		float distance = Vector3.Distance(target.position, transform.position);
		Enemy_health health_script=enemy.GetComponent<Enemy_health>();
        if(health_script.enemy_hurting){
            anim.SetTrigger("hurt");
        }
		if(health_script.health>0 && target!=null){
	    anim.SetBool("idle",true);
		anim.SetBool("run",false);
		anim.SetBool("shoot",false);
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
		else if(Vector3.Distance(enemy.position,temp)<=5){
			anim.SetBool("run",false);
			anim.SetBool("idle",true);
			anim.SetBool("shoot",false);
		}
		else if(Vector3.Distance(target.position, transform.position) >lookRadius){
			anim.SetBool("run", true);
			anim.SetBool("idle",false);
			anim.SetBool("shoot",false);
			Enemy_health health_script=enemy.GetComponent<Enemy_health>();
			if(health_script.health>0){
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

		float distance = Vector3.Distance(target.position, transform.position);
		Enemy_health health_script=enemy.GetComponent<Enemy_health>();
		if(health_script.health>0){
		if (distance <= lookRadius || health_script.enemy_hurting)
			{
				// Move towards the target
				agent.SetDestination(target.position);
				anim.SetBool("idle",false);
				anim.SetBool("run", true);
				anim.SetBool("shoot",false);
				// If within attacking distance
				if (distance <= agent.stoppingDistance)
				{    
					target_enemy tp=agent.GetComponent<target_enemy>();
					anim.SetBool("run",false);
					 if ((Time.time > nextPlayTime))
  					  {  
							if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
								anim.SetBool("run",true);
								anim.SetBool("idle",false);
								tp.enemy_targeting();
								anim.SetBool("shoot",true);
								shoot_temp=true;
								audioSource.PlayOneShot(attack_sound);
                                attack_ads.Play();
								nextPlayTime = Time.time + playRate;
							}
							else{
								anim.SetBool("run",false);
								anim.SetBool("idle",false);
								anim.SetBool("shoot",true);
								shoot_temp=true;
								tp.enemy_targeting();
                                 attack_ads.Play();
								audioSource.PlayOneShot(attack_sound);
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