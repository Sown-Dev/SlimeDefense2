using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour{
    public RectTransform rt;
    public float rate = 1;
    void Update(){
        rt.anchoredPosition = new Vector2((Time.time % 0.5f)*32*rate, (Time.time % 0.5f)*32*rate);
    }
}
