using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class score_kill : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemies;
    public GameObject[] initial_enemies;
    public TMP_Text kill,grenade;
    public front_canvas_score_kill_script fpsk;
    public TMP_Text timer;
    private GameObject[] grenades;
    private GameObject time_finish,enemy_finish;
    private float initial_timevalue=1200;
    public float timevalue=1200;
    // Update is called once per frame
    void Start(){
        initial_enemies=GameObject.FindGameObjectsWithTag("enemy");
        fpsk.initial_enemies_length=initial_enemies.Length;
    }
    void Update()
    {
        enemies=GameObject.FindGameObjectsWithTag("enemy");
        fpsk.enemies_length=enemies.Length;
        grenades=GameObject.FindGameObjectsWithTag("grenade");
        grenade.text=grenades.Length.ToString()+"/"+"8";
        kill.text= "Kill:" + (initial_enemies.Length-enemies.Length).ToString() + "/"+initial_enemies.Length.ToString();
        if(enemies.Length==0){
            enemy_finish = GameObject.FindGameObjectWithTag("enemy_finish");
            if(enemy_finish==null){
             Instantiate(Resources.Load("enemy_finish") as GameObject);
            }
        }

        if(timevalue>0){
            timevalue-=Time.deltaTime;
        }
        else{
            timevalue=0;
              time_finish = GameObject.FindGameObjectWithTag("time_finish");
            if(time_finish==null){
             Instantiate(Resources.Load("time_finish") as GameObject);
            }
        }
        if(timevalue<30){
            timer.color=new Color32(255,0,27,255);
        }
        fpsk.time_left=initial_timevalue-timevalue;
        DisplayTime(timevalue);
    }

    void DisplayTime(float timetodisplay){
        if(timetodisplay<0){
            timetodisplay=0;
        }
        float minutes=Mathf.FloorToInt(timetodisplay/60);
        float seconds=Mathf.FloorToInt(timetodisplay%60);
        timer.text=string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
