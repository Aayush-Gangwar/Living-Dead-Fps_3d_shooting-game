using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_barrel : MonoBehaviour
{
    public float barrel_health=100f;
    private Transform own;
    public float hurt_radius=4f;
    public AudioClip explode_;
    public AudioSource barrel_aduio;
    private GameObject barrel_smoke;
    private GameObject[] enemies;
    private GameObject[] barrels;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        barrel_smoke=Resources.Load<GameObject>("fly_enemy_death");
        own=GetComponent<Transform>();
    }

    public void TakeDamage(){
        if(barrel_health>0){
            barrel_health-=20f;
        }
        else{
             StartCoroutine(after_dead_animation());
             barrel_aduio.PlayOneShot(explode_);
             Invoke("dead_",1f);
        }
    }
private void dead_(){
    Destroy(gameObject);
       enemies=GameObject.FindGameObjectsWithTag("enemy");
        foreach(var kit in enemies){
	        float dis = Vector3.Distance(own.position,kit.transform.position);
            if(dis<=hurt_radius && barrel_health==0){
                Enemy_health eh=kit.transform.GetComponent<Enemy_health>();
                eh.TakeDamage(40f);
                eh.enemy_hurting=true;
            }
    }

    barrels=GameObject.FindGameObjectsWithTag("ExplosiveBarrel");
        foreach(var kit in barrels){
	        float dis = Vector3.Distance(own.position,kit.transform.position);
            if(dis<=hurt_radius && barrel_health==0){
                explosion_barrel eh=kit.transform.GetComponent<explosion_barrel>();
                eh.barrel_health=0f;
                eh.TakeDamage();
            }
    }
}
    private IEnumerator after_dead_animation(){
  yield  return new WaitForSeconds(0f);
   GameObject _effect=Instantiate(barrel_smoke,own.position,Quaternion.identity);
    GameObject _effect2=Instantiate(barrel_smoke,own.position,Quaternion.identity);
     GameObject _effect3=Instantiate(barrel_smoke,own.position,Quaternion.identity);
   Destroy(_effect,3f);
   Destroy(_effect2,3f);
   Destroy(_effect3,3f);
}

}
