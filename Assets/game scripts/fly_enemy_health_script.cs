using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class fly_enemy_health_script : MonoBehaviour
{
    public float health=100f;
    Transform player;
    Transform own;
    bool isPlayingAnim=true;
    private NavMeshAgent nvm;
    public float cooldown_time=0f;
    public float cooling_rate=5f;
    public float recharge_health=3f;
    public AudioSource dyingsource;
    public Collider eye_collider;
    public health_bar_script hbs;
    private GameObject fly_enemy_death_smoke;
    bool is_attacking=false;
    public bool enemy_hurting;
    public AudioClip fly_enemy_dying;
     private float false_rest=3f;
     private float Count=0f;

    private float max_health=100f;

    public void TakeDamage(float amount){
        if(health>0){
         health-=amount;
         is_attacking=false;
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
   GameObject _effect=Instantiate(fly_enemy_death_smoke,own.position,Quaternion.identity);
    GameObject _effect2=Instantiate(fly_enemy_death_smoke,own.position,Quaternion.identity);
     GameObject _effect3=Instantiate(fly_enemy_death_smoke,own.position,Quaternion.identity);
   Destroy(_effect,3f);
   Destroy(_effect2,3f);
   Destroy(_effect3,3f);

}


private IEnumerator PlayAnim () {
    isPlayingAnim = true;
    dyingsource.PlayOneShot(fly_enemy_dying);
    StartCoroutine(after_dead_animation());
    yield return new WaitForSeconds(1f);
 
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
       fly_enemy_death_smoke=Resources.Load<GameObject>("fly_enemy_death");
       own=GetComponent<Transform>(); 
        fly_enemy_dying=Resources.Load<AudioClip> ("fly_enemy_dying");
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
            Invoke("Die",0);
    }
}
}
