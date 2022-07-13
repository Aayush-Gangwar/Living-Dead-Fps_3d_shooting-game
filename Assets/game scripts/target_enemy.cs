using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class target_enemy : MonoBehaviour
{
    public float damage=5f;
    public float attack_range =100f;
    public GameObject gun;
    public GameObject player;
    private NavMeshAgent nvm;

    // Update is called once per frame
    void Update(){
        nvm=GetComponent<NavMeshAgent>();
        attack_range=nvm.stoppingDistance;
        }
    public void enemy_targeting()
    {   
        player = GameObject.FindGameObjectWithTag("Player");
        Enemy_health health_script=GetComponent<Enemy_health>();
        if(health_script!=null){
        if(player!=null && health_script.health>0){
            shoot();
        }
        }
        else{
             fly_enemy_health_script fly_health_script=GetComponent<fly_enemy_health_script>();
                if(player!=null && fly_health_script.health>0){
                    shoot();
        }
        }
    }
    private void shoot(){
        RaycastHit hit;
        if(Physics.Raycast(gun.transform.position,gun.transform.forward,out hit,attack_range)){
            player_health health_script=hit.transform.GetComponent<player_health>();
            if(health_script!=null){
                health_script.TakeDamage(damage);
            }
        }
    }
}
