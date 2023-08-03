 using UnityEngine;

 [CreateAssetMenu(fileName = "StatusEffectUpgrade", menuName = "ScriptableObjects/CustomUpgrades/StatusEffect Upgrade")]  
 public class StatusEffectUpgradeSO: UpgradeSO{
     public StatusEffectUpgrade su;
     
      public override Upgrade u{
         get{ return su; }
         set{ su = value as StatusEffectUpgrade; }
     }

 }
