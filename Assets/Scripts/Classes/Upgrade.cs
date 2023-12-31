﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Upgrade{
    public string Name;
    [TextArea(8, 10)] public string Description;
    [HideInInspector] public UpgradePool Pool;
    
    public bool Base = true; //whether or not upgrade is a base upgrade
    public bool Retain = false; //whether or not upgrade is removed from pool
    public Stats st;

    [Header("Children Upgrades:")]
    [SerializeReference] public List<UpgradeSO> Children; //list of upgrades that are unlocked by this upgrade

    public Sprite Icon;

    [HideInInspector] public Player player; //reference to player debuffs

    public Upgrade(Upgrade u){
        st = u.st;
        Name = u.Name;
        Description = u.Description;
        Icon = u.Icon;
        Retain = u.Retain;
        Base = u.Base;
        Pool = u.Pool;
        Children = u.Children;
        player = u.player;
        
    }
    public Upgrade(){
    }
    
    public virtual void Init(Player p){
        player = p;

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