using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BulletsOnKillUpgrade : Upgrade{

    public int amount;
    
    public override void Init(Debuffs d){
        base.Init(d);
        Slime.OnDeath+= SpawnBullets;
    }

    public void SpawnBullets(Slime s){
        float rot = Random.Range(0, 360);
        for (int i = 0; i < amount; i++){
            Player.p.s.SpawnBullet(s.transform.position, new Vector3(0,0,(rot +(i*360f/3 )) ), 0);
        }
    }

    public BulletsOnKillUpgrade(Upgrade u) : base(u){ }
}