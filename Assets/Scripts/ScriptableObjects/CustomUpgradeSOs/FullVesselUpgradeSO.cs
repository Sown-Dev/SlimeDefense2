using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/CustomUpgrades/FullVessel Upgrade")]  
public class FullVesselUpgradeSO: UpgradeSO{
    public  FullVesselUpgrade su;
     
    public override Upgrade u{
        get{ return su; }
        set{ su = ( FullVesselUpgrade)value; }
    }

}