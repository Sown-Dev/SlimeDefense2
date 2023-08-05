using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/CustomUpgrades/HPperkills Upgrade")]  
public class HealthPerKillUpgradeSO: UpgradeSO{
    public HealthPerKillUpgrade su;
     
    public override Upgrade u{
        get{ return su; }
        set{ su = (HealthPerKillUpgrade)value; }
    }

}