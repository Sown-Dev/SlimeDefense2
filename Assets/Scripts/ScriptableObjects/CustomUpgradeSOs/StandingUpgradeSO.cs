 using UnityEngine;

 [CreateAssetMenu(fileName = "StandingUpgrade", menuName = "ScriptableObjects/CustomUpgrades/Standing Upgrade")]  
 public class StandingUpgradeSO: UpgradeSO{
     public StandingUpgrade su;
     
      public override Upgrade u{
         get{ return su; }
         set{ su = value as StandingUpgrade; }
     }

 }
