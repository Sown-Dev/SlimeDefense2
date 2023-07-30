using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour{
    [Header("References")] public Upgrade selectedUpgrade;
    public UpgradeManager um;
    public CanvasGroup cg;
    public TMP_Text selectedName;
    public TMP_Text selectedDesc;
    public Image selectedIcon;

    public bool open;

    public Transform options;
    public GameObject optionPrefab;
    public List<UpgradeOptionUI> optionList;

    private void Awake(){
        cg.alpha = 0;
    }

    public void Update(){
        cg.alpha = Mathf.Lerp(cg.alpha, open ? 1 : 0, Time.unscaledDeltaTime * 10f);
        cg.interactable = open;
        cg.blocksRaycasts = open;
    }

    public void Open(List<Upgrade> ulist){
        Time.timeScale = 0f;
        open = true;

        //clear old options
        foreach (Transform child in options){
            Destroy(child.gameObject);
        }

        //create new options
        foreach (Upgrade u in ulist){
            GameObject go = Instantiate(optionPrefab, options);
            UpgradeOptionUI uoui = go.GetComponent<UpgradeOptionUI>();
            uoui.myUI = this;
            uoui.myUpgrade = u;
            optionList.Add(uoui);
            uoui.OnClick();
        }
    }

    public void Close(){
        open = false;
        Time.timeScale = 1f;
    }

    public void Select(Upgrade s){
        selectedUpgrade = s;
        selectedName.text = s.Name;
        selectedDesc.text = s.Description;
        selectedIcon.sprite = s.Icon;
    }

    public void Upgrade(){
        if (selectedUpgrade != null){
            um.AddUpgrade(selectedUpgrade);
            Close();
        }
    }
}