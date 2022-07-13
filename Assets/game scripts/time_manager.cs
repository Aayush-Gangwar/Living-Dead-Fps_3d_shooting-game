using UnityEngine;

public class time_manager : MonoBehaviour {

	public float slowdownFactor = 0.05f;
	public float slowdownLength = 2f;
    private GameObject[] enemies,in_enemy;
    private int count=0;

void Start(){
    in_enemy=GameObject.FindGameObjectsWithTag("enemy");
}
	void Update ()
	{
        enemies=GameObject.FindGameObjectsWithTag("enemy");
        GameObject enemy=enemies[0];
        Enemy_health eh=enemy.GetComponent<Enemy_health>();
        fly_enemy_health_script eh2=enemy.GetComponent<fly_enemy_health_script>();
        if(count==1){
            Invoke("time_normal",0.8f);
        }

        if(eh!=null){
        if(enemies.Length==1 && eh.health<=5 && count==0){
            DoSlowmotion();
            count+=1;
        }
        }
        else{
            if(enemies.Length==1 && eh2.health<=5 && count==0){
            DoSlowmotion();
            count+=1;
        }
        }
	}

	public void DoSlowmotion ()
	{
		Time.timeScale = slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}
 
 private void time_normal(){
            Time.timeScale=1f;
 }
}