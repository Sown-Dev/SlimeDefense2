using System;

[Serializable]
public class Statistic{
    public Statistic(){
        type = Stats.Statstype.Damage;
        amount = 0;
        operation = Stats.StatsOperation.Add;
    }

    public Statistic(Stats.Statstype _type, double amt, Stats.StatsOperation op){
        type = _type;
        amount = amt;
        operation = op;
    }
    
    public Stats.Statstype type;
    public double amount;
    public Stats.StatsOperation operation;
}