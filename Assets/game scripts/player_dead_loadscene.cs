using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class player_dead_loadscene : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject time_finish,enemy_finish;
    public player_health ph;
    // Update is called once per frame
    void Update()
    {
         time_finish = GameObject.FindGameObjectWithTag("time_finish");
         enemy_finish = GameObject.FindGameObjectWithTag("enemy_finish");
         if(ph.player_dead==true && time_finish==null && enemy_finish==null){ 
            SceneManager.LoadScene("game over 1");
            Cursor.visible=true;
            Cursor.lockState=CursorLockMode.None;
         }
        else if(ph.player_dead==false && time_finish==null && enemy_finish!=null){
          Invoke("load_scene",10);
        }
        else if(ph.player_dead==false && time_finish!=null && enemy_finish==null){
             SceneManager.LoadScene("game over 1");
             Cursor.visible=true;
              Cursor.lockState=CursorLockMode.None;
        }
        else if(ph.player_dead==false && time_finish!=null && enemy_finish!=null){
          Invoke("load_scene",10);
        }
    }

    private void load_scene(){
       SceneManager.LoadScene("won scene 1");
     Cursor.visible=true;
     Cursor.lockState=CursorLockMode.None;
    }
}
