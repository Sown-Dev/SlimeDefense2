using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/CustomUpgrades/Vampire Upgrade")]  
public class VampireUpgradeSO: UpgradeSO{
    public VampireUpgrade su;
     
    public override Upgrade u{
        get{ return su; }
        set{ su = (VampireUpgrade)value; }
    }

}