using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class KillClipUpgrade : Upgrade{
    public StatusEffect seBase;

    public override void Init(Debuffs d){
        base.Init(d);
        Slime.OnDeath+= ApplyKillClip;
    }

    public void ApplyKillClip(Slime s){
        debuffs.AddDebuff( new StatusEffect(seBase)); //could reset timer but i'm just gonna add the time
    }

    public KillClipUpgrade(Upgrade u) : base(u){ }
}