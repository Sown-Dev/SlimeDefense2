using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class OneTapUpgrade : Upgrade{
    
    public override void Init(Player player){
        base.Init(player);
        Shooting.OnSpawnBullet+= OneTap;
    }

    public void OneTap(int ammoRemaining, Player player, Bullet bullet){
        if (ammoRemaining == Convert.ToInt32(player.finalStats[Stats.Statstype.AmmoCapacity])){
            bullet.Damage*= 1.8f;
            bullet.transform.localScale *=1.4f;
            bullet.Speed *= 1.1f;
            bullet.Penetration+=1;
            bullet.color += new Color(0, -0.29f, -0.29f);
            Debug.Log("bigbullet!");
        }
    }
    public override void Remove(){
        Shooting.OnSpawnBullet-= OneTap;
    }

    public OneTapUpgrade(Upgrade u) : base(u){ }
}