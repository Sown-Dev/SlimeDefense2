using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class StandingUpgrade : Upgrade{
    public StatusEffect seBase;

    public override void Init(Player player){
        base.Init(player);
        Movement.OnStandStill+= ApplyBuff;
    }

    public void ApplyBuff( Vector3 pos){
        player.debuffs.AddDebuff( new StatusEffect(seBase)); //could reset timer but i'm just gonna add the time
    }
    public override void Remove(){
        Movement.OnStandStill+= ApplyBuff;
    }

    public StandingUpgrade(Upgrade u) : base(u){ }
}