using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mode_menu : MonoBehaviour
{

    // Update is called once per frame

    public void zombie_mode(){
        SceneManager.LoadScene("level_2_loader");
    }
    public void normal_mode(){
        SceneManager.LoadScene("level_loader");
    }
}
