using System;
using UnityEngine;


[CreateAssetMenu(fileName = "UpgradeSO", menuName = "ScriptableObjects/UpgradeSO", order = 1)]
[Serializable]
public class UpgradeSO: ScriptableObject{

    public Upgrade u;
        
    
    
    public bool Unlocked = true;

}