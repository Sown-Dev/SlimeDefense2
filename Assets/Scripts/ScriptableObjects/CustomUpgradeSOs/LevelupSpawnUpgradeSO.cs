using UnityEngine;

[CreateAssetMenu(fileName = "SpawnUpgrade", menuName = "ScriptableObjects/CustomUpgrades/LevelupSpawn Upgrade")]  
public class LevelupSpawnUpgradeSO: UpgradeSO{
    public LevelupSpawnUpgrade su;
     
    public override Upgrade u{
        get{ return su; }
        set{ su = value as LevelupSpawnUpgrade; }
    }

}