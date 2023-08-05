using System;
using UnityEngine;

[Serializable]
public class BulletExplodeUpgrade : Upgrade{
    public  float radius =0.8f;
    public  float  force =300f;
    public float chance = 0.3f;
    
    public override void Init(Player player){
        Bullet.OnBulletHit += Explode;
        base.Init(player);
    }

    public void Explode(float damage, Vector2 hitPoint, Slime s){
        if (Utils.Random(chance)){
            Utils.u.CreateExplosion(hitPoint, 300, damage * 0.5f, 0.8f);
        }
        
    }
    public override void Remove(){
        Bullet.OnBulletHit -= Explode;
    }


    public BulletExplodeUpgrade(Upgrade u) : base(u){ }
}