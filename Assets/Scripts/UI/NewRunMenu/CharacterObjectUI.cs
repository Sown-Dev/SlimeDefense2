using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterObjectUI : MonoBehaviour, IPointerClickHandler{
    public CharacterSO cso;
    public TMP_Text name;
    public TMP_Text title;
    public Image icon;
    public Image bg;

    public bool Selected;
    [HideInInspector] public RectTransform rt;

    private Vector2 originalSize;
    

    private void Awake(){
        rt = gameObject.GetComponent<RectTransform>();
        originalSize = rt.sizeDelta;
    }

    void Start(){
        name.text = cso.c.name;
        title.text = "\"" +cso.c.title + "\"";
        icon.sprite = cso.c.icon;
        bg.color = cso.c.bgcolor;
    }

    private Vector2 prevSD;
    private void Update(){
        Vector2 tvec=Vector2.Lerp(rt.sizeDelta, Selected ? new Vector2( originalSize.x*1.5f, originalSize.y) : originalSize, 14 * Time.deltaTime);
        rt.sizeDelta = new Vector2((int)tvec.x, (int)tvec.y);
        rt.localScale = Vector3.Lerp(rt.localScale, Selected ? Vector3.one *1.2f :Vector3.one, 14 * Time.deltaTime);
        
        if (prevSD != rt.sizeDelta){
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
        }

        prevSD = tvec;

    }

    public void OnPointerClick(PointerEventData eventData){
        CharacterStatsUI.csui.SelectNew(this);
    }
}
