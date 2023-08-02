using System;
using UnityEngine;

public class BleedDebuff: StatusEffect{
    public BleedDebuff(float dur, float str) : base(dur, str){
        particlePrefab = Resources.Load("DebuffParticles/Poison") as GameObject;
        type = Debuffs.DebuffTypes.Poison;
    }

    private float TimeElapsed = 0;
    public override void Tick(IDamagable hp, IStatusEffectable st){
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed >= 1){
            hp.TakeDamage(hp.maxHealth * 0.05f * strength);
            TimeElapsed = 0;
        }
        base.Tick(hp, st);
    }
}