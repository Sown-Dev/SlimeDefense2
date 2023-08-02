﻿using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
//Same as empty, but gains bonuses for having more health
public class FullVesselUpgrade : Upgrade{

    public float multHealth = 10; // multiplier for health
    public Stats baseStats;
    public override void Init(Debuffs d){
        base.Init(d);
        Player.OnUpdateStats += FixStats;
    }

    public void FixStats(Player p, UpgradeManager um){
        this.st = (Stats)(baseStats.Clone()) * ((p.Health)/multHealth);
    }

    public FullVesselUpgrade(Upgrade u) : base(u){ }
}