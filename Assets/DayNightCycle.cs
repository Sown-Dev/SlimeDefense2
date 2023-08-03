using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour{

    public GameManager gm;
    public AnimationCurve ac;
    public Light2D globalLight;

    public float dayDuration = 300;

    void Awake(){
       // ac = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    }
    public void Update(){
        globalLight.intensity = ac.Evaluate((float)(gm.TimeElapsed / dayDuration));
        
    }
}
