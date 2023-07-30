using System;
using Unity.VisualScripting;

[Serializable]
public class Character: ICloneable{
    public Stats stats;
    public string name;
    public object Clone(){
        Character c = new Character();
        c.name = this.name;
        c.stats = (Stats)this.stats.Clone();
        return c;
    }
}