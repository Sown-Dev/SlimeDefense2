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
            bullet.Damage*= 2;
            bullet.transform.localScale *=1.4f;
            bullet.Speed *= 1.1f;
            bullet.Penetration+=2;
            bullet.color += new Color(0, -0.25f, -0.25f);
            Debug.Log("bigbullet!");
        }
    }

    public OneTapUpgrade(Upgrade u) : base(u){ }
}