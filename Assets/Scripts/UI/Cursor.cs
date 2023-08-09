using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour{
    public static Cursor cr;
    public Animator am;

    public bool mode = false;
    private void Awake(){
        cr = this;
        UnityEngine.Cursor.visible = false;
    }

    private void Update(){
        var screenPoint = (Input.mousePosition);
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        if (Input.GetMouseButton(1)){
            UnityEngine.Cursor.visible = false;

        }
    }

    public void setMode(bool newMode){
        mode = newMode;
        am.SetBool("Mode",mode);
    }
}