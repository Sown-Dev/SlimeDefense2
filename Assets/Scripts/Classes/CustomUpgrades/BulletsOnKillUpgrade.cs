using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BulletsOnKillUpgrade : Upgrade{

    public int amount;
    public float dmgMult=0.8f;
    
    public override void Init(Player player){
        base.Init(player);
        Slime.OnDeath+= SpawnBullets;
    }

    public void SpawnBullets(Slime s){
        float rot = Random.Range(0, 360);
        for (int i = 0; i < amount; i++){
            Player.p.s.SpawnBullet(s.transform.position, new Vector3(0,0,(rot +(i*360f/3 )) ), 0).Damage *= dmgMult;
        }
    }
    public override void Remove(){
        Slime.OnDeath-= SpawnBullets;
    }
    
    public BulletsOnKillUpgrade(Upgrade u) : base(u){ }
}