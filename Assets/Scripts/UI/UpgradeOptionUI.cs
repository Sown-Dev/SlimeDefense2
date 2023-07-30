using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOptionUI : MonoBehaviour{
    public Image myImg;
    public bool selected;

    public UpgradeUI myUI;
    public Upgrade myUpgrade;

    public void Update(){
        if(myUpgrade==null) return;
        myImg.sprite = myUpgrade.Icon;
        
    }

    public void OnClick(){
        myUI.Select(myUpgrade);
        selected = true;
    }

}
