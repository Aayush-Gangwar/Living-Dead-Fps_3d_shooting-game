using UnityEngine;

public class time_manager_2 : MonoBehaviour {

	public float slowdownFactor = 0.05f;
	public float slowdownLength = 2f;
    private GameObject[] enemies,in_enemy;
    private int count=0;

void Start(){
    in_enemy=GameObject.FindGameObjectsWithTag("enemy");
}
	void Update ()
	{
        if(count==1){
            count=0;
            Invoke("time_normal",0.5f);
        }
        enemies=GameObject.FindGameObjectsWithTag("enemy");
        if(enemies.Length%5==0){
        foreach(var en in enemies){
        Enemy_health eh=en.GetComponent<Enemy_health>();
        fly_enemy_health_script eh2=en.GetComponent<fly_enemy_health_script>();
        if(eh!=null){
            if(eh.enemy_hurting && count==0 && eh.health<=5f){
                count=1;
                DoSlowmotion();
            }
        }
        else{
            if(eh2.enemy_hurting && count==0  && eh.health<=5f){
                count=1;
                DoSlowmotion();
            }
        }
        }
	}
    // else{
    //     Time.timeScale=1f;
    // }
    }

	public void DoSlowmotion ()
	{
		Time.timeScale = slowdownFactor;
		// Time.fixedDeltaTime = Time.timeScale * .02f;
	}
 
 private void time_normal(){
            Time.timeScale=1f;
            // count=0;
 }
}