using System;
using UnityEngine;

[Serializable]
public class ReloadDashUpgrade : Upgrade{

    public float force;
    
    public override void Init(Player player){
        base.Init(player);
       Shooting.OnReload += Dash;
    }

    public void Dash(int ammoRemaining, Shooting s){
        player.rb.AddForce(s.towardsMouse.normalized * force, ForceMode2D.Impulse);
    }

    public override void Remove(){
        Shooting.OnReload -= Dash;
    }

    public ReloadDashUpgrade(Upgrade u) : base(u){ }
}