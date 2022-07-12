using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class game_over_script :MonoBehaviour{
    // Start is called before the first frame update
    public AudioSource ads;
    public front_canvas_score_kill_script sk;
    public TMP_Text score;
    // Update is called once per frame
    void Start(){
        ads.Play();
    }
    public void Replay(){
          SceneManager.LoadScene("level_loader");
    }
    public void quit(){
        Application.Quit();
    }
    void Update()
    {
        score.text= "KILL:" + (sk.initial_enemies_length-sk.enemies_length).ToString()+"/"+sk.initial_enemies_length.ToString();
    }
}
