using System;
using UnityEngine;

[Serializable]
public class PoisonDebuffUpgrade : Upgrade{
    
    
    public override void Init(Debuffs d){
        Bullet.OnBulletHit += Poison;
        base.Init(d);
        Shooting.OnSpawnBullet+= AddColor;
    }

    public void AddColor(int ammoRemaining, Player player, Bullet bullet){
        bullet.color += new Color(-0.2f, 0, -0.2f); //subtract negatives
    }
    public void Poison(float damage, Vector2 hitPoint, Slime s){
        if (Utils.Random(0.5f)){
            s.ApplyStatusEffect(new PoisonDebuff(5, 1));
        }
        
    }

    public PoisonDebuffUpgrade(Upgrade u) : base(u){ }
}