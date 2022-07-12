using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    // SceneManager.GetActiveScene().buildIndex+1
    public AudioSource ads;
    void Start(){
        ads.Play();
    }
    // public void playGame(){
    //     SceneManager.LoadScene("level_loader");
    // }
    public void quit(){
        Application.Quit();
    }
}
