using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BiomassUpgrade : Upgrade{
    
    public float chance;

    public override void Init(Player player){
        base.Init(player);
        Slime.OnDeath+= AddAmmo;
    }

    public void AddAmmo(Slime s){
        if (Utils.Random(chance))
        {
            if(Player.p.s.currentAmmo<Player.p.finalStats[Stats.Statstype.AmmoCapacity])
                Player.p.s.currentAmmo++;

        }
    }

    public override void Remove(){
        Slime.OnDeath-= AddAmmo;
    }

    public BiomassUpgrade(Upgrade u) : base(u){
    }
}