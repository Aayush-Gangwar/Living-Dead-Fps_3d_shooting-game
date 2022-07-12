using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class game_won_script : MonoBehaviour
{
    public AudioSource ads;
    public TMP_Text timer;
    private float timetodisplay;
    public front_canvas_score_kill_script fpsk;
    void Start(){
        ads.Play();
         timetodisplay=fpsk.time_left;
         if(timetodisplay<0){
            timetodisplay=0;
        }
        float minutes=Mathf.FloorToInt(timetodisplay/60);
        float seconds=Mathf.FloorToInt(timetodisplay%60);
        timer.text="Time Taken:"+string.Format("{0:00}:{1:00}",minutes,seconds);
    }
    public void Replay(){
          SceneManager.LoadScene("level_loader");
    }
    public void quit(){
        Application.Quit();
    }
}
