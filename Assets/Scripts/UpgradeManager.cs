using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class UpgradeManager : MonoBehaviour{
    [Header("CustomUpgrades")]
    public BulletExplodeUpgrade exp;
    public PoisonDebuffUpgrade pois;
    public KillClipUpgrade killclip;
    public StandingUpgrade bipod;
    public StandingUpgrade riotshield;
    public StandingUpgrade fungus;
    public StandingUpgrade sentry;
    public StandingUpgrade fort;
    public OneTapUpgrade onetap;
    
    
    [Header("CustomUtilityUpgrades")]
    public BiomassUpgrade biomass;

    public TrainingUpgrade training;
    
    public static UpgradeManager um;
    public Player player;
    [Header("UI")]
    public UpgradeViewer upgradeViewer;
    public UpgradeUI upgradeUI;

    
    
    
    public List<Upgrade> allUpgrades;
    public List<Upgrade> LevelUpgradePool;
    public List<Upgrade> UtilityUpgradePool;
    public List<Upgrade> MedUpgradePool;
    
    public Queue<List<Upgrade>> UpgradeQueue = new Queue<List<Upgrade>>(); //queue of upgrades to be given to player after 1st

    [HideInInspector] public Dictionary<UpgradePool, List<Upgrade>> Pools = new Dictionary<UpgradePool, List<Upgrade>>();
    private void Awake(){
        um = this;
        //Debug.Log(Resources.LoadAll<UpgradeSO>("ScriptableObjects/Upgrades").Length);
        //allUpgrades = Resources.LoadAll<UpgradeSO>("ScriptableObjects/Upgrades").Select(x => x.u).ToList();
        LevelUpgradePool = Resources.LoadAll<UpgradeSO>("ScriptableObjects/Upgrades/Level").Select(x => x.u).ToList();
        LevelUpgradePool.Add(exp);
        LevelUpgradePool.Add(pois);
        LevelUpgradePool.Add(killclip);
        LevelUpgradePool.Add(bipod);
        LevelUpgradePool.Add(riotshield);
        LevelUpgradePool.Add(fungus);
        LevelUpgradePool.Add(sentry);
        LevelUpgradePool.Add(fort);
        LevelUpgradePool.Add(onetap);
        
        
        UtilityUpgradePool = Resources.LoadAll<UpgradeSO>("ScriptableObjects/Upgrades/Utility").Select(x => x.u).ToList();

        UtilityUpgradePool.Add(biomass);
        UtilityUpgradePool.Add(training);

        
        MedUpgradePool = Resources.LoadAll<UpgradeSO>("ScriptableObjects/Upgrades/Med Crate").Select(x => x.u).ToList();


        Pools[UpgradePool.Level] = LevelUpgradePool;
        Pools[UpgradePool.Utility] = UtilityUpgradePool;
        Pools[UpgradePool.Med] = MedUpgradePool;
        
        //add pool to upgrade
        foreach(Upgrade u in LevelUpgradePool){
            u.Pool = UpgradePool.Level;
        }
        foreach(Upgrade u in UtilityUpgradePool){
            u.Pool = UpgradePool.Utility;
        }
        foreach(Upgrade u in MedUpgradePool){
            u.Pool = UpgradePool.Med;
        }
    }
    
    //randomly get x upgrades no duplicates
    public List<Upgrade> GetUpgrades(int amt, List<Upgrade> pool){
        //randomize list
        pool = pool.OrderBy(x => Guid.NewGuid()).ToList();
        List<Upgrade> returnList = new List<Upgrade>();
        for(int i = 0; i<amt; i++){
            returnList.Add(pool[i]);
        }

        return returnList;
    }

    public void UpgradeObtain(int amount){
        List<Upgrade> toObtain = GetUpgrades(amount, LevelUpgradePool);
        if (!upgradeUI.open){
            upgradeUI.Open(toObtain);
        }
        else{
            UpgradeQueue.Enqueue(toObtain);
        }
    }

    public void UtilityUpgradeObtain(){
        List<Upgrade> toObtain = GetUpgrades(3, UtilityUpgradePool);
        if (!upgradeUI.open){
            upgradeUI.Open(toObtain);
        }
        else{
            UpgradeQueue.Enqueue(toObtain);
        }
    }
    public void MedUpgradeObtain(){
        List<Upgrade> toObtain = GetUpgrades(1, MedUpgradePool);
        if (!upgradeUI.open){
            upgradeUI.Open(toObtain);
        }
        else{
            UpgradeQueue.Enqueue(toObtain);
        }
    }

    public void AddUpgrade(Upgrade u){
        player.Upgrades.Add(u);
        u.Init(player.debuffs);
        player.CalculateStats();
        player.Heal(u.HealOnPickup);
        if (UpgradeQueue.Count > 0){
            upgradeUI.Open(UpgradeQueue.Dequeue());
        }
        upgradeViewer.UpdateIcons();

        //removes upgrade from pool and adds its children if it has any
        Pools[u.Pool].Remove(u);
        if (u.Children != null){
            foreach (Upgrade child in u.Children){
                Pools[u.Pool].Add(child);
            }
        }

    }
}
