using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Upgrade{
    public string Name;
    [TextArea(15, 20)] public string Description;
    [HideInInspector] public UpgradePool Pool;
    
    public bool Base = true; //whether or not upgrade is a base upgrade
    public Stats st;

    [Header("Children Upgrades:")]
    [SerializeReference] public List<UpgradeSO> Children; //list of upgrades that are unlocked by this upgrade

    public Sprite Icon;

    [HideInInspector] public Debuffs debuffs; //reference to player debuffs

    public Upgrade(Upgrade u){
        st = u.st;
        Name = u.Name;
        Description = u.Description;
        Icon = u.Icon;
    }
    public Upgrade(){
    }
    
    public virtual void Init(Debuffs d){
        debuffs = d;

    }

    public virtual void Remove(){
        
    }
    
    public float HealOnPickup = 0;
    
}

public enum UpgradePool{
    Level=0,
    Utility=1,
    Med=2,
}