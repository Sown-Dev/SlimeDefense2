using UnityEngine;

[CreateAssetMenu(fileName = "SpawnUpgrade", menuName = "ScriptableObjects/CustomUpgrades/Spawn Upgrade")]  
public class SpawnUpgradeSO: UpgradeSO{
    public SpawnUpgrade su;
     
    public override Upgrade u{
        get{ return su; }
        set{ su = value as SpawnUpgrade; }
    }

}