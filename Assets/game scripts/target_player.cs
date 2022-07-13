using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_player : MonoBehaviour
{
    public float damage=10f;
    private float range =100f;
    public GameObject gun;
    public GameObject checking;

    public GameObject gun_big,hand_gun;
    private GameObject bloodeffect;

    private Vector3 temp;
    private Transform z_body;
    // Update is called once per frame

    void Update()
    {   
        if(gun_big.activeSelf==true){
            range=18f;
        }
        else{
            range=8f;
        }
        checking = GameObject.FindGameObjectWithTag("checking");
        player_health health_player=transform.GetComponent<player_health>();
        if((Input.GetMouseButtonDown(0)) && checking!=null && health_player.health>0){
            shoot();
        }
    }
    private void shoot(){
        RaycastHit hit;
        if(Physics.Raycast(gun.transform.position,gun.transform.forward,out hit,range)){
            Enemy_health health_script=hit.transform.GetComponent<Enemy_health>();
            if(health_script!=null){
                health_script.enemy_hurting=true;
                if(hit.collider==health_script.head){
                    z_body=health_script.head.transform;
                    temp=z_body.position;
                    StartCoroutine(blood_effect());
                    health_script.TakeDamage(80);
                    if(health_script.head_loss!=null){
                    Destroy(health_script.head_loss);
                    }
                }
                else{
                    z_body=health_script.blood_point;
                    temp=z_body.position;
                    StartCoroutine(blood_effect());
                    health_script.TakeDamage(damage);
                }
            }
            else{
                fly_enemy_health_script fly_health_script=hit.transform.GetComponent<fly_enemy_health_script>();
                if(fly_health_script!=null){
                    fly_health_script.enemy_hurting=true;
                    if(hit.collider==fly_health_script.eye_collider){
                        fly_health_script.TakeDamage(80);
                    }
                    else{
                    fly_health_script.TakeDamage(damage);
                    }
                }
            }
            explosion_barrel exp_bar=hit.transform.GetComponent<explosion_barrel>();
            if(exp_bar!=null){
                exp_bar.TakeDamage();
            }
        }
    }
    private IEnumerator blood_effect(){
          yield  return new WaitForSeconds(0f);
    bloodeffect=Resources.Load<GameObject>("blood");
   GameObject _effect=Instantiate(bloodeffect,temp,Quaternion.identity);
   Destroy(_effect,1f);
    }
}
