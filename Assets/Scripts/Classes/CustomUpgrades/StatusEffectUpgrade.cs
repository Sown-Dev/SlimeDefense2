using System;
using UnityEngine;

[Serializable]
public class StatusEffectUpgrade : Upgrade
{
    public float chance = 0.5f;
    public StatusEffect se;
    public Color color;
    
    public override void Init(Debuffs d){
        Bullet.OnBulletHit += Debuff;
        base.Init(d);
        Shooting.OnSpawnBullet+= AddColor;
    }

    public void AddColor(int ammoRemaining, Player player, Bullet bullet)
    {
        if (color != null)
            bullet.color += color;
    }
    public void Debuff(float damage, Vector2 hitPoint, Slime s){
        if (Utils.Random(chance)){
            s.ApplyStatusEffect(se);
        }
        
    }

    public StatusEffectUpgrade(Upgrade u) : base(u){ }
}