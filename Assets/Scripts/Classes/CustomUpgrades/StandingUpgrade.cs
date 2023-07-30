﻿using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class StandingUpgrade : Upgrade{
    public StatusEffect seBase;

    public override void Init(Debuffs d){
        base.Init(d);
        Movement.OnStandStill+= ApplyBuff;
    }

    public void ApplyBuff( Vector3 pos){
        debuffs.AddDebuff( new StatusEffect(seBase)); //could reset timer but i'm just gonna add the time
    }

    public StandingUpgrade(Upgrade u) : base(u){ }
}