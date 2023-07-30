using System;
using UnityEngine;

[Serializable]
public class Upgrade{
    public Stats st;
    public string Name;

    [TextArea(15, 20)] public string Description;
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