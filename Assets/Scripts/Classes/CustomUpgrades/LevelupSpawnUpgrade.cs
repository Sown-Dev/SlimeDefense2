using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class LevelupSpawnUpgrade : Upgrade{
    
    public GameObject prefab;
    private List<GameObject> instances;
    public override void Init(Player p){
        instances = new List<GameObject>();
        base.Init(p);
        Player.OnLevelUp += Spawn;
    }

    public void Spawn(Player p, UpgradeManager um){
        GameObject go = GameObject.Instantiate(prefab, p.transform.position, Quaternion.identity);
        go.GetComponent<SpawnItemScript>().Init(p);
        instances.Add(go);
    }
    

    public override void Remove(){
        foreach (GameObject go in instances){
            GameObject.Destroy(go);
        }
        base.Remove();
    }


    public LevelupSpawnUpgrade(Upgrade u) : base(u){ }
}