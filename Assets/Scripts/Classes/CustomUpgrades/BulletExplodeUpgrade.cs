using System;
using UnityEngine;

[Serializable]
public class BulletExplodeUpgrade : Upgrade{
    private static float radius =0.8f;
    private static float  force =300f;
    
    public override void Init(Debuffs d){
        Bullet.OnBulletHit += Explode;
        base.Init(d);
    }

    public void Explode(float damage, Vector2 hitPoint, Slime s){
        if (Utils.Random(0.2f)){
            Utils.u.CreateExplosion(hitPoint, 300, damage * 0.5f, 0.8f);
        }
        
    }

    public BulletExplodeUpgrade(Upgrade u) : base(u){ }
}