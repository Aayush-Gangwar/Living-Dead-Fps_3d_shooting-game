using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade_script : MonoBehaviour
{
    public float delay=3f;
    public float grenade_radius=5f;
    private GameObject grenade_explosion;
    public float explosion_force=700f;
    public AudioClip grenade_;
    public AudioSource grenade_audio;
    public bool hasexplode=false;
    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        grenade_audio.Stop();
        countdown=delay;
        grenade_explosion=Resources.Load<GameObject>("fly_enemy_death");
    }

    // Update is called once per frame
    void Update()
    {
        countdown-=Time.deltaTime;
        if(countdown<=0 && !hasexplode){
            hasexplode=true;
             grenade_audio.PlayOneShot(grenade_);
            explode();
        }
    }
    private void die(){
        Destroy(gameObject);
    }
    public void explode(){
        GameObject effect= Instantiate(grenade_explosion,transform.position,Quaternion.identity);
        Destroy(effect,3f);
        Collider[] collider=Physics.OverlapSphere(transform.position,grenade_radius);
        foreach(Collider nearby in collider){
            // Rigidbody rb= nearby.GetComponent<Rigidbody>();
            // if(rb!=null){
            //     rb.AddExplosionForce(explosion_force,transform.position,grenade_radius);
            // }

            Enemy_health health1=nearby.GetComponent<Enemy_health>();
            fly_enemy_health_script health2=nearby.GetComponent<fly_enemy_health_script>();
            player_health health3=nearby.GetComponent<player_health>();
            explosion_barrel exp1=nearby.GetComponent<explosion_barrel>();
            if(health1!=null){
                health1.TakeDamage(150f);
            }
             if(health2!=null){
                health2.TakeDamage(100f);
            }
             if(exp1!=null){
                exp1.barrel_health=0f;
                exp1.TakeDamage();
            }
             if(health3!=null){
                health3.TakeDamage(5f);
            }
        }
        Invoke("die",1f);
    }
}
