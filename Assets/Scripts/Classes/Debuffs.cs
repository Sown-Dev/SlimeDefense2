using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Debuffs{
    public Transform parent;
    public enum DebuffTypes{
        Poison = 1,
        Mark = 2,
        Slow = 3,
        Stun = 4,
        Regen = 5,
        Stats = 6,
    }

    public List<StatusEffect> debuffs;

    public Debuffs(Transform p){
        debuffs = new List<StatusEffect>();
        parent = p;
    }
    
    public void Tick(IDamagable hp, IStatusEffectable st){
        foreach (StatusEffect se in debuffs.ToList()){
            se.duration -= Time.deltaTime;
            se.Tick(hp, st);
            if(se.duration<=0){
                se.Remove();
                debuffs.Remove(se);
            }
        }
    }

    public Stats applyStats(Stats stats){
        foreach (StatusEffect se in debuffs){
            if (se.isStats){
                stats.Combine(se.stats);
            }
        }

        return stats;
    }

    public void AddDebuff(StatusEffect se){
        
            bool found = false;
            foreach (StatusEffect v in debuffs){
                if (v.type == se.type && v.strength == se.strength ){
                    if (se.isStats){
                        if (v.stats == se.stats){
                            AddEffects(v, se);
                            found = true;
                            break;
                        }
                    }
                    else{
                        AddEffects(v, se);
                        found = true;
                        break;
                    }
                }

            }
            if (!found){
                debuffs.Add(se);
                se.Init(parent);
            }
        
        
    }
    public bool this[DebuffTypes type] => debuffs.Find(t => t.type == type) != null;

    public StatusEffect Get(DebuffTypes type){
        return debuffs.Find(t => t.type == type);
    }

    public StatusEffect AddEffects(StatusEffect a, StatusEffect b){
        if (a.addStrength){
            a.strength += b.strength;
        }
        else{
            a.duration += b.duration;
        }

        return a;
    }
    
}