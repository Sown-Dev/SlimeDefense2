using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class HealthPerKillUpgrade : Upgrade{

    public float amount;
    private int kills = 0;
    public override void Init(Player player){
        base.Init(player);
        Slime.OnDeath+= Heal;
    }

    public void Heal(Slime s){
        Player.p.Heal(amount);
    }

    public HealthPerKillUpgrade(Upgrade u) : base(u){ }
}