using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/CustomUpgrades/EmptyVessel Upgrade")]  
public class EmptyVesselUpgradeSO: UpgradeSO{
    public  EmptyVesselUpgrade su;
     
    public override Upgrade u{
        get{ return su; }
        set{ su = ( EmptyVesselUpgrade)value; }
    }

}