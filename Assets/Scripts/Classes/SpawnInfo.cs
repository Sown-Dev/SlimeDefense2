using System;
using UnityEngine;

[Serializable]
public class SpawnInfo{
    public string name;
    public int CreditCost;
    public float SpawnTime;
    public GameObject Prefab;

    public SpawnInfo(){
        name = "";
        CreditCost = 0;
        SpawnTime = 0;
    }
}