using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pause_menu_script : MonoBehaviour
{
   public bool gamepaused=false;
//    public front_canvas_score_kill_script spk;
   public AudioSource ads1,ads2;
   public GameObject pausemenu_ui;

void Start(){
    ads1.Play();
}
    // Update is called once per frame
    void Update()
    {

        if(gamepaused){
             if(ads1.isPlaying){
                 ads1.Stop();
             }
              if(ads2.isPlaying){
                 ads1.Stop();
             }
             else{
             ads2.Play();
             }
            }
        else{
            if(ads2.isPlaying){
                ads2.Stop();
            }
            if(ads1.isPlaying){
                ads2.Stop();
            }
            else{
            ads1.Play();
            }
            }



       if(gamepaused){
           if(Input.GetKeyDown(KeyCode.Tab)){
                Time.timeScale=1f;
                SceneManager.LoadScene("game");
        }
       }
        if(gamepaused){
           if(Input.GetKeyDown(KeyCode.Q)){
                Time.timeScale=1f;
                Application.Quit();
        }
       }
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gamepaused){
                resume();
            }
            else{
                pause();
            }
        }
        
    }
    public void resume(){
        pausemenu_ui.SetActive(false);
        Time.timeScale=1f;
        gamepaused=false;
    }
    public void pause(){
        pausemenu_ui.SetActive(true);
        Time.timeScale=0f;
        gamepaused=true;
    }
}
