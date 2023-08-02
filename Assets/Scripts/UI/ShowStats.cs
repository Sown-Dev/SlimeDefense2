using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Weapon equipped;
    public CanvasGroup cg;
    private float alpha;

    public TMP_Text tt;
    public TMP_Text tname;

    public Player p;

   

    public void OnPointerEnter(PointerEventData eventData){
        alpha = 1;
    }

    private void Update(){
        equipped = p.Weapon;
        tname.text =  p.Weapon.Name;
        /*tt.text =
            "Damage: " + equipped.damage + "\n" +
            "ROF: " + equipped.rpm + "\n" +
            "Reload: " + equipped.reloadTime + "s\n" +
            "Mag Size: " + equipped.maxAmmo + "";*/
        
        //tooltip
        //cg.alpha = Mathf.Lerp(cg.alpha, alpha, 25 * Time.deltaTime);
    }

    public void OnPointerExit(PointerEventData eventData){
        alpha = 0;
    }
}