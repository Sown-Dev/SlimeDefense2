using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/CustomUpgrades/Reload Dash Upgrade")]  
public class ReloadDashUpgradeSO: UpgradeSO{
    public ReloadDashUpgrade su;
     
    public override Upgrade u{
        get{ return su; }
        set{ su = value as ReloadDashUpgrade; }
    }

}