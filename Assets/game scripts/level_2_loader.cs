using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class level_2_loader : MonoBehaviour
{
    public Slider slider;
    private float next_load_time=1f;

    void Start(){
        slider.maxValue=300f;
        StartCoroutine(loadasyn());
    }

    IEnumerator loadasyn(){
        float progress=1f;
while(slider.value!=slider.maxValue){
     slider.value=progress;
     progress+=1f;
    if(slider.value==slider.maxValue){
        SceneManager.LoadSceneAsync("zombie_level");
    }
    yield return null;
}
    }
}
