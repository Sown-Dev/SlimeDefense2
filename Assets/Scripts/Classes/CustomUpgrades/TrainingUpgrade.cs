using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class TrainingUpgrade : Upgrade
{
    public Upgrade toGive;

    public override void Init(Debuffs d){
        base.Init(d);
        Player.OnLevelUp += AddUpgrade;
    }

    public void AddUpgrade(Player p, UpgradeManager um){
        //give upgrade
        um.AddUpgrade(toGive);
    }

    public TrainingUpgrade(Upgrade u) : base(u){ }
}