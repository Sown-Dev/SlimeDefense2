using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class HealthPerKillUpgrade : Upgrade{

    public float amount;
    private int kills = 0;
    public int killThreshold = 10;
    public override void Init(Player player){
        kills = 0;
        base.Init(player);
        Slime.OnDeath+= Heal;
    }

    public void Heal(Slime s){
        kills++;
        if (kills >= killThreshold){
            kills = 0;
            Player.p.Heal(amount);
        }
    }
    
    public override void Remove(){
        Slime.OnDeath+= Heal;
    }


    public HealthPerKillUpgrade(Upgrade u) : base(u){ }
}