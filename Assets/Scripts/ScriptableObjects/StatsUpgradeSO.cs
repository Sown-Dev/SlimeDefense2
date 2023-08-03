using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "UpgradeSO", menuName = "ScriptableObjects/StatsUpgradeSO", order = 1)]
[Serializable]
public class StatsUpgradeSO: UpgradeSO{

    public Upgrade su;

    public override Upgrade u{
        get{ return su; }
        set{ su = value; }
    }


    //on editor refresh reassign var
    private void OnValidate(){
        AssetDatabase.SaveAssets();

    }


}