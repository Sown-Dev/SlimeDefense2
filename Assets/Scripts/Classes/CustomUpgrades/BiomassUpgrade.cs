using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BiomassUpgrade : Upgrade{
    public float chance;

    public override void Init(Debuffs d){
        base.Init(d);
        Slime.OnDeath+= AddAmmo;
    }

    public void AddAmmo(Slime s){
        if(Utils.Random(chance))
            Player.p.s.currentAmmo++;
    }

    public BiomassUpgrade(Upgrade u) : base(u){ }
}