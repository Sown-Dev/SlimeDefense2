using System;
using System.Collections.Generic;

[Serializable]
public class StatsDict : SerializableDictionary<Stats.Statstype, Statistic>{ } //dumb shit

[Serializable]
public class Stats : ICloneable{
    public List<Statistic> stats;


    public Stats(){
        stats = new List<Statistic>();
    }

    public Stats(int i){
        stats = new List<Statistic>();
        foreach (Statstype t in Enum.GetValues(typeof(Statstype))){
            stats.Add(new Statistic(t, i, StatsOperation.Multiply));
        }
    }

    public enum Statstype{
        Damage = 1,
        MaxHealth = 2,
        MoveSpeed = 3,
        AttackSpeed = 4,
        Projectiles = 5,
        AmmoCapacity = 6,
        ReloadTime = 7,
        ProjSpeed = 8,
        XPGain = 9,
        AmmoUsage =10,
        BulletSize=11,
        Penetration=12,
        Knockback = 13,
        Regeneration = 14,
        Spread=15,
        BulletLifeTime=16,
        LevelUpgradeSlots=17,
        Resistance=18,
        FOV=19,
        Recoil=20,
        KnockbackRecieved=21,
        GoldMultiplier=22,
        HPMultiplier=23,
        BackProjectiles=24,
    }

    public enum StatsOperation{
        Multiply = 1,
        Add = 2,
    }

    public Stats Combine(Stats toCombine){
        /*foreach (KeyValuePair<Statstype, Statistic> e  in toCombine.stats){
            if (this.stats[e.Key] != null){
                if (e.Value.operation == StatsOperation.Add){
                    this.stats[e.Key].amount += e.Value.amount;
                }
                if(e.Value.operation == StatsOperation.Multiply){
                    this.stats[e.Key].amount *= e.Value.amount ;
                }
            }else{
                this.stats[e.Key] = e.Value;
            }
        }
        return this;*/
        foreach (var e in toCombine.stats){
            bool found = false;
            foreach (var f in this.stats){
                if (f.type == e.type){
                    found = true;
                    //if both multiply, end result is multiply, if one is add and one mult, end is add, else add
                    // ( * * = * ) ; ( * + = + ; + * = + ) ; ( + + = + )
                    if (e.operation == StatsOperation.Multiply && f.operation == StatsOperation.Multiply){
                        f.amount *= e.amount;
                        f.operation = StatsOperation.Multiply;
                    }
                    else if (e.operation == StatsOperation.Multiply || f.operation == StatsOperation.Multiply){
                        f.amount *= e.amount;
                        f.operation = StatsOperation.Add;
                    }
                    else if (e.operation == StatsOperation.Add && f.operation == StatsOperation.Add){
                        f.amount += e.amount;
                        f.operation = StatsOperation.Add;
                    }
                    //doesn't break in case you have multiple stats of the same type
                }
            }

            if (!found){
                this.stats.Add(e);
            }
        }

        return this;
    }

    //rewrite this for null case
    public float this[Statstype stat]{
        get{
            if (this.stats.Find(t => t.type == stat) != null){
                return (float)this.stats.Find(t => t.type == stat).amount;
            }
            else{
                return 0;
            }

        }
        
    }

    public object Clone(){
        Stats s = new Stats();
        foreach (var e in this.stats){
            s.stats.Add(new Statistic(){ type = e.type, amount = e.amount, operation = e.operation });
        }

        return s;
    }

    public static Stats operator *(Stats a, float b){
        foreach (var e in a.stats){
            e.amount *= b;
        }

        return a;
    }
}