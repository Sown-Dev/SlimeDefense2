using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnUpgrade : Upgrade{
    
    public GameObject prefab;
    private GameObject instance;
    public override void Init(Player p){
        base.Init(p);
        instance =GameObject.Instantiate(prefab, p.transform);
        instance.GetComponent<SpawnItemScript>().Init(p);
        
    }

    public override void Remove(){
        GameObject.Destroy(instance);
        base.Remove();
    }
    

    public SpawnUpgrade(Upgrade u) : base(u){ }
}