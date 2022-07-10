using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_health : MonoBehaviour
{
    public float health=100f;
    public Animator anim;
    Transform player;
    Transform own;
    public bool isPlayingAnim=true;
    private NavMeshAgent nvm;
    public float cooldown_time=0f;
    public float cooling_rate=5f;
    public float recharge_health=3f;
    private AudioSource audioSource;
	public AudioClip enemy_hurt;
    public Transform blood_point;
    public Collider head;
    public GameObject head_loss;
    public health_bar_script hbs;
    private GameObject enemy_death_smoke;
    bool is_attacking=false;
    public bool enemy_hurting;
    public AudioClip enemy_dying;
     private float false_rest=3f;

    private float max_health=100f;
    private float Count=0f;

    public void TakeDamage(float amount){
        if(health>0){
         health-=amount;
         is_attacking=false;
         audioSource.PlayOneShot(enemy_hurt);
        }
         else  if(health<=0){
             enemy_hurting=false;
             nvm=GetComponent<NavMeshAgent>();
             nvm.enabled=false;
             is_attacking=true;
             StartCoroutine(PlayAnim());
         }
    }
    private void Die(){
        Destroy(gameObject);
    }

private IEnumerator after_dead_animation(){
  yield  return new WaitForSeconds(0f);
   GameObject _effect=Instantiate(enemy_death_smoke,own.position,Quaternion.identity);
    GameObject _effect2=Instantiate(enemy_death_smoke,own.position,Quaternion.identity);
     GameObject _effect3=Instantiate(enemy_death_smoke,own.position,Quaternion.identity);
   Destroy(_effect,6f);
   Destroy(_effect2,6f);
   Destroy(_effect3,6f);

}


private IEnumerator PlayAnim () {
    isPlayingAnim = true;
    anim = GetComponent<Animator>();
    anim.SetTrigger("die");
    audioSource.PlayOneShot(enemy_dying);
    StartCoroutine(after_dead_animation());
    yield return new WaitForSeconds(5.1f);
 
    isPlayingAnim = false;
}
 
    private bool enemy_hurt_false(bool x ){
        if(Time.time>false_rest){
            x=false;
            false_rest=Time.time+1f;
        }
        return x;
    }
// Called every frame
void Update () {
    enemy_hurting=enemy_hurt_false(enemy_hurting);
    hbs.setmaxhealth(max_health);
    hbs.sethealth(health);
       audioSource=GetComponent<AudioSource>();
       enemy_death_smoke=Resources.Load<GameObject>("after_zombie_death");
       enemy_hurt=Resources.Load<AudioClip> ("enemy_hurt");
       own=GetComponent<Transform>(); 
       if(own.GetComponent<soldier_enemyController>()==null){
         enemy_dying=Resources.Load<AudioClip> ("zombie_dying");
       }
       else{
            enemy_dying=Resources.Load<AudioClip> ("enemy_dying");
       }
        player  = GameObject.FindGameObjectWithTag("playergun").transform;
        
    if(health>0 && health+recharge_health<=100 && Time.time>cooldown_time && is_attacking==false){
        health+=recharge_health;
        cooldown_time=Time.time+cooling_rate;
    }
    if(health<=0){
        enemy_hurting=false;
        if(Count==0){
         nvm=GetComponent<NavMeshAgent>();
             nvm.enabled=false;
             is_attacking=true;
             StartCoroutine(PlayAnim());
             Count+=1;
        }
    }
    if (!isPlayingAnim && health<=0) {
        anim.speed=0;
        audioSource.Pause();
             anim.SetBool("run",false);
             anim.SetBool("shoot",false);
             anim.SetBool("idle",false);
            Invoke("Die",0);
    }
}
}
