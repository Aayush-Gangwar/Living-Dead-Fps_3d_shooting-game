using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player_health : MonoBehaviour
{
    public float health=100f;
    bool isPlayingAnim=true;
    private float max_health=100f;
    public bool player_dead=false;

    // public float cooldown_time=0f;
    // public float cooling_rate=5f;
    // public float recharge_health_f=3f;
    public Image hurt_image1,hurt_image2,hurt_image3;
    public AudioSource audioSource;
	public AudioClip player_hurt;
    public AudioClip player_dying;
    public health_bar_script hbs;
    private GameObject[] medikits;
    private Transform player;
    private GameObject used_kit;
    bool recharge_health;
    // public bool is_attack_player=false;

    public void TakeDamage(float amount){
        if(health>0){
         health-=amount;
        //  is_attack_player=true;
         audioSource.PlayOneShot(player_hurt);
        }
         else  if(health<=0){
            // is_attack_player=false;
             StartCoroutine(PlayAnim());
         }
    }
    private void Die(){
        Destroy(gameObject);
    }

private IEnumerator PlayAnim () {
    isPlayingAnim = true;
    audioSource.PlayOneShot(player_dying);
    yield return new WaitForSeconds(3f);
    player_dead=true;
    isPlayingAnim = false;
}
 
 void hurt_image_fn(){
     Color red_scatter=hurt_image1.color;
     red_scatter.a=1-(health/max_health);
     hurt_image1.color=red_scatter;
     hurt_image2.color=red_scatter;
     hurt_image3.color=red_scatter;
 }

// Called every frame
void Update () {
    hurt_image_fn();
    hbs.setmaxhealth(max_health);
    hbs.sethealth(health);
    player_hurt=Resources.Load<AudioClip> ("player_hurt");
    player_dying=Resources.Load<AudioClip> ("player_dying");
    player= GameObject.FindGameObjectWithTag("Player").transform;
	medikits= GameObject.FindGameObjectsWithTag("medikit");
    // if(health>0 && health+recharge_health_f<=100 && Time.time>cooldown_time && is_attack_player==false){
    //     health+=recharge_health_f;
    //     cooldown_time=Time.time+cooling_rate;
    // }

	foreach(var kit in medikits){
	float dis = Vector3.Distance(player.position,kit.transform.position);
	if(dis<2.5f && health!=max_health && Input.GetKeyDown(KeyCode.CapsLock)){
    used_kit=kit;
	recharge_health=true;
	}
    }
    if(recharge_health){
        health=100f;
        Destroy(used_kit);
        recharge_health=false;
    }

    // if (health<=0 && isPlayingAnim==false) {
    //     audioSource.Pause();
    //     Invoke("Die",0);
    // }
}
}
