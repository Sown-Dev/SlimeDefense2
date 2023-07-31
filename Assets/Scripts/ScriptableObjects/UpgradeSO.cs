using System;
using UnityEngine;


[CreateAssetMenu(fileName = "UpgradeSO", menuName = "ScriptableObjects/UpgradeSO", order = 1)]
[Serializable]
public class UpgradeSO: ScriptableObject{
    public Upgrade u;
    public string type;
    public bool Unlocked = true;


    private void OnValidate(){
        type = u.GetType().ToString();
        
        if (u.Name == null || u.Name == ""){
            u.Name = this.name;
        }

        if (u.Name == "Exp"){
            Debug.Log("changed it!!");
            u = new BulletExplodeUpgrade(u);
        }
    }
}