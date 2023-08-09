using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Character: ICloneable{
    public Stats stats;
    public string name;
    public string title;
    public List<UpgradeSO> upgrades;
    
    [TextArea(3, 4)]public string perks;
    public Sprite icon;
    public Color bgcolor;
    
    
    
    public object Clone(){
        Character c = new Character();
        c.name = this.name;
        c.stats = (Stats)this.stats.Clone();
        c.title = this.title;
        c.perks = this.perks;
        c.icon = this.icon;
c.bgcolor = this.bgcolor;

        return c;
    }
}