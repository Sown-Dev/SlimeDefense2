using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EmptyVesselUpgrade : Upgrade{

    public float multHealth = 10; // multiplier for health
    public Stats baseStats;
    public override void Init(Player player){
        base.Init(player);
        Player.OnUpdateStats += FixStats;
    }

    public void FixStats(Player p, UpgradeManager um){
        this.st = (Stats)(baseStats.Clone()) * ((p.finalStats[Stats.Statstype.MaxHealth]-p.Health)/multHealth);
    }

    public EmptyVesselUpgrade(Upgrade u) : base(u){ }
}