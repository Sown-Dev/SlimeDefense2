using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class TrainingUpgrade : Upgrade
{
    public Upgrade toGive;

    public override void Init(Player player){
        base.Init(player);
        Player.OnLevelUp += AddUpgrade;
    }

    public void AddUpgrade(Player p, UpgradeManager um){
        //give upgrade
        um.AddUpgrade(toGive);
    }
    public override void Remove(){
        Player.OnLevelUp -= AddUpgrade;
    }

    public TrainingUpgrade(Upgrade u) : base(u){ }
}