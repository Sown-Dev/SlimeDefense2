using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class VampireUpgrade : Upgrade{

    public float amount;
    
    public override void Init(Debuffs d){
        base.Init(d);
        Slime.OnDeath+= Heal;
    }

    public void Heal(Slime s){
        Player.p.Heal(amount);
    }

    public VampireUpgrade(Upgrade u) : base(u){ }
}