using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeViewer : MonoBehaviour{
    public bool open;
    public CanvasGroup cg;
    public GameObject upgradePrefab;
    public Transform upgradesParent;
    
    public Player p;

    private void Awake(){
        UpdateIcons();
        open = true;
    }

    void Update()
    {
        cg.alpha = Mathf.Lerp(cg.alpha, open ? 1 : 0, Time.unscaledDeltaTime * 10f);
        cg.interactable = open;
        cg.blocksRaycasts = open;
        
        if (Input.GetKeyDown(KeyCode.Tab)){
            open = !open;
        }
    }

    public void UpdateIcons(){
        foreach (Transform child in upgradesParent){
            Destroy(child.gameObject);
        }
        foreach (Upgrade u in p.Upgrades){
            Instantiate(upgradePrefab,upgradesParent).GetComponent<UpgradeIcon>().Icon.sprite = u.Icon;
        }
    }
}
