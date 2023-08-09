using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour{
    public static DayNightCycle dnc;
    public GameManager gm;
    public AnimationCurve ac;
    public Light2D globalLight;

    public float dayDuration = 300;

    void Awake(){
        dnc = this;
    }
    public void Update(){
        globalLight.intensity = ac.Evaluate((float)(gm.TimeElapsed / dayDuration));
        
    }
}
