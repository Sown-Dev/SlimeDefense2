/*using System;
using UnityEngine.Windows;

[Serializable]
public class OldStats{
    private Statstype type;
    public double Damage;
    public double MaxHealth;
    public double MoveSpeed;
    public double AttackSpeed;
    public double Projectiles; //is a float because could be used for multipliers. is rounded eventually
    public double AmmoCapacity;
    public double ReloadTime;
    public double ProjSpeed;
    public double XPGain;


    public Stats(){
        type = Statstype.Add;
        Damage = 0;
        MaxHealth = 0;
        MoveSpeed = 0;
        AttackSpeed = 0;
        Projectiles = 0;
        AmmoCapacity = 0;
        ReloadTime = 0;
        ProjSpeed = 0;
        XPGain = 0;
    }

    public static Stats operator +(Stats one, Stats two){
        Stats sum = new Stats();
        sum.Damage = one.Damage + two.Damage;
        sum.MaxHealth = one.MaxHealth + two.MaxHealth;
        sum.MoveSpeed = one.MoveSpeed + two.MoveSpeed;
        sum.AttackSpeed = one.AttackSpeed + two.AttackSpeed;
        sum.Projectiles = one.Projectiles + two.Projectiles;
        sum.AmmoCapacity = one.AmmoCapacity + two.AmmoCapacity;
        sum.ReloadTime = one.ReloadTime + two.ReloadTime;
        sum.ProjSpeed = one.ProjSpeed + two.ProjSpeed;
        sum.XPGain = one.XPGain + two.XPGain;
        return sum;
    }

    public static Stats operator *(Stats one, Stats two){
        Stats mult = new Stats();
        if (one.Damage != 0 && two.Damage != 0)
            mult.Damage = one.Damage * two.Damage;
        if (one.MaxHealth != 0 && two.MaxHealth != 0)
            mult.MaxHealth = one.MaxHealth * two.MaxHealth;
        if (one.MoveSpeed != 0 && two.MoveSpeed != 0)
            mult.MoveSpeed = one.MoveSpeed * two.MoveSpeed;
        if (one.AttackSpeed != 0 && two.AttackSpeed != 0)
            mult.AttackSpeed = one.AttackSpeed * two.AttackSpeed;
        if (one.Projectiles != 0 && two.Projectiles != 0)
            mult.Projectiles = one.Projectiles * two.Projectiles;
        if (one.AmmoCapacity != 0 && two.AmmoCapacity != 0)
            mult.AmmoCapacity = one.AmmoCapacity * two.AmmoCapacity;
        if (one.ReloadTime != 0 && two.ReloadTime != 0)
            mult.ReloadTime = one.ReloadTime * two.ReloadTime;
        if (one.ProjSpeed != 0 && two.ProjSpeed != 0)
            mult.ProjSpeed = one.ProjSpeed * two.ProjSpeed;
        if (one.XPGain != 0 && two.XPGain != 0)
            mult.XPGain = one.XPGain * two.XPGain;

        return mult;
    }

    public static Stats CombineStats(Stats newStats, Stats baseStats){
        Stats output = new Stats();
        if (newStats.type == Statstype.Add){
            output = newStats + baseStats;
        }
        else if (newStats.type == Statstype.Multiply){
            output = newStats * baseStats;
        }

        return output;
    }

    
}*/