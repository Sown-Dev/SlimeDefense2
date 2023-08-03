using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeviceUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public bool Open;
    
    public CanvasGroup cg;

    private Vector2 startPos;
    private Vector2 downPos;

    private Vector2 toPos;

    public RectTransform rt;

    public TerrainSpawner ts;    
    
    //refs
    public Image cross;
    public TMP_Text text;
    public CanvasGroup textbox;
    public CanvasGroup textboxFlicker;

    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    
    //vars

    public bool hasSignal;
    public bool hasText;

    public Transform player;
    [HideInInspector] public Transform consolePos;

    private void Awake(){
        rt= GetComponent<RectTransform>();
        startPos = rt.anchoredPosition;
        
        downPos = new Vector2(startPos.x, startPos.y-340);

        rt.anchoredPosition = downPos;
        
        NoSignal();
    }

    private void Start(){
        consolePos = ts.radioInstance.transform;
    }

    private bool showText;
    private float tElapsed;
    private void Update(){
        toPos = Open ? startPos : downPos;
        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, toPos, Time.unscaledDeltaTime * 10f);

        tElapsed += Time.unscaledDeltaTime;
        if ( tElapsed> 0.5f){
            tElapsed = 0;
            showText = !showText;
        }
        textboxFlicker.alpha = showText ? 1: 0;
        textbox.alpha= hasText? 1:0;

        cross.color = hasSignal ? Color.white : Color.clear;
        if (hasSignal){
            Vector2 dir = (consolePos.position - player.position).normalized;
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)){
                cross.sprite = dir.x > 0 ? right : left;
            }
            else{
                cross.sprite = dir.y > 0 ? up : down;
            }
        }


    }

    public void OnPointerEnter(PointerEventData eventData){
        Open = true;
    }

    public void OnPointerExit(PointerEventData eventData){
        Open = false;
    }

    public void NoSignal(){
        hasSignal = false;
        hasText = true;
        text.text = "no signal";
    }

    public void AcquireSignal(){
        hasSignal = true;
        hasText = true;
        text.text = "signal acquired";
    }
    public void RemoveSignal(){
        hasSignal = false;
        hasText = true;
        text.text = "signal lost";
    }


    public void OnPointerClick(PointerEventData eventData){
        if(hasSignal)
            hasText = false;
    }
}
