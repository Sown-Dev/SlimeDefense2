using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeChild : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Upgrade myU;

    public Image icon;
    
    private void Start(){
        icon.sprite = myU.Icon;
    }
    
    public void OnPointerEnter(PointerEventData eventData){
        UpgradeToolTip.tt.Show = true;
        UpgradeToolTip.tt.UpdateTT(myU);
    }

    public void OnPointerExit(PointerEventData eventData){
        UpgradeToolTip.tt.Show = false;
    }
}
