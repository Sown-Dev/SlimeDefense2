using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class VampireUpgrade : Upgrade{

    public float amount;
    
    public override void Init(Player player){
        base.Init(player);
        Slime.OnDeath+= Heal;
    }

    public void Heal(Slime s){
        Player.p.Heal(amount);
    }
    public override void Remove(){
        Slime.OnDeath-= Heal;
    }

    public VampireUpgrade(Upgrade u) : base(u){ }
}