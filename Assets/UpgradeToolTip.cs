using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeToolTip : MonoBehaviour{
    public static UpgradeToolTip tt;

    public TMP_Text Name;
    public TMP_Text Desc;
    public CanvasGroup cg;

    public bool Show = false;

    private void Awake(){
        tt = this;
        cg.alpha = 0;
    }

    private void Update(){
        var screenPoint = (Input.mousePosition);
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        cg.alpha =(float)Math.Round(Mathf.Lerp(cg.alpha, Show ? 1 : 0, Time.unscaledDeltaTime * 10f),3);
    }


    public void UpdateTT(Upgrade u){
        Name.text = u.Name;
        Desc.text = u.Description;
    }
}