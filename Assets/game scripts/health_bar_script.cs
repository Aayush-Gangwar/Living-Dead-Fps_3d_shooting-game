using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health_bar_script : MonoBehaviour
{
    public Slider slider;
    public Gradient grd;
    public  Image fill;

    public void setmaxhealth(float health){
        slider.maxValue=health;
        fill.color=grd.Evaluate(1f);
    }

    public void sethealth(float health){
        slider.value=health;
        fill.color=grd.Evaluate(slider.normalizedValue);
    }
}

