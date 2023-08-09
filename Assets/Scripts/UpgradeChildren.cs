using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeChildren : MonoBehaviour{
    public UpgradeUI uui;
    public Transform list;
    public GameObject upgradeChildPrefab;
    public CanvasGroup cg;

    public Upgrade prev;

    private bool open;

    private void Start(){
        cg.alpha = 0;
    }

    public void Update(){
        open = uui.open && uui.selectedUpgrade.Children?.Count > 0;

        cg.alpha = (float)Math.Round(Mathf.Lerp(cg.alpha, open ? 1 : 0, Time.unscaledDeltaTime * 10f),3);
        cg.interactable = open;
        cg.blocksRaycasts = open;
        
        if (open && prev != uui.selectedUpgrade){
            UpdateChildren();
        }

        prev = uui.selectedUpgrade;
    }


    public void UpdateChildren(){
        foreach (Transform child in list){
            Destroy(child.gameObject);
        }

        if (uui.selectedUpgrade.Children?.Count > 0){
            foreach (var uso in uui.selectedUpgrade.Children){
                Instantiate(upgradeChildPrefab, list).GetComponent<UpgradeChild>().myU = uso.u;
            }
        }
    }
}