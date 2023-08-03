using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public delegate void LevelUp(Player p, UpgradeManager um);
public delegate void Update(Player p, UpgradeManager um);
public delegate void UpdateStats(Player p, UpgradeManager um);

public class Player : MonoBehaviour, IFriendlyDamagable, IStatusEffectable
{
    public static event UpdateStats OnUpdateStats;
    public static event LevelUp OnLevelUp;
    public static event Update OnUpdate;
    
    public Debuffs debuffs{ get; set; }
    
    [Header("UI")] public Image xpBar;
    public TMP_Text xpText;
    public Image hpBar;
    public TMP_Text hpText;
    public Image weaponIcon;

    
    [Header("PlayerStats")] public static Player p;
    public WeaponSO myWeapon;
    public Weapon Weapon;
    public CharacterSO myChar;
    public Character character;
    public Stats finalStats;
    public Rigidbody2D rb;

    public float Health{ get; set; }
    public float maxHealth{ get; set; }


    [Header("Utils")] public LayerMask Enemies;
    public CinemachineVirtualCamera cmvc;
    public Shooting s;

    public void CalculateStats(){
        OnUpdateStats?.Invoke(this, UpgradeManager.um);  //calculated before rewriting stats, meaning that it uses previously calculated stats
        finalStats = new Stats(1);
        finalStats.Combine(character.stats);
        finalStats.Combine(Weapon.baseStats);
        foreach (var upgrade in Upgrades){
            finalStats.Combine(upgrade.st);
        }
        foreach (var status in debuffs.debuffs){
            if (status.isStats){
                finalStats.Combine( status.stats*status.strength);
            }
        }
        

        UpdateHPUI();
        cmvc.m_Lens.OrthographicSize = 6.8f* finalStats[Stats.Statstype.FOV];
    }
    
    void Awake(){
        debuffs = new Debuffs(transform);
        
        character = (myChar.c);
        Weapon = myWeapon.w;
        Init();
        CalculateStats();

        
        
        
        Health= finalStats[Stats.Statstype.MaxHealth];
        
        xpBar.fillAmount = currentXp / maxXp;
        xpText.text = currentXp + "/" + maxXp;
        Slime.OnDeath += GainXP;
        rb = GetComponent<Rigidbody2D>();
        p = this;
        UpdateHPUI();
        
        weaponIcon.sprite = Weapon.sprite;
        UpdateGoldTxt();

    }


    private float RegenElapsed = 0;
    private int prevDebuffCount = 0;
    void Update(){
        OnUpdate?.Invoke(this, UpgradeManager.um);
        debuffs.Tick(this,this);

        if (debuffs.debuffs.Count != prevDebuffCount){
            CalculateStats();
        }
        prevDebuffCount=debuffs.debuffs.Count; //triggers on add and remove status effects
        
        RegenElapsed += Time.deltaTime;
        if(RegenElapsed>=20 && finalStats[Stats.Statstype.Regeneration]>1){
            RegenElapsed = 0;
            Heal(finalStats[Stats.Statstype.Regeneration]);
        }
        xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, (currentXp / maxXp), Time.unscaledDeltaTime * 10f);
        
        
    }
    
    //set start values
    void Init(){
        currentXp = 0;
        level = 0;
        maxXp = 100f;
    }
    
    //Upgrades & level
    [Header("Upgrades & Level")] public List<Upgrade> Upgrades = new List<Upgrade>();


    public float currentXp;
    public float maxXp;
    public int level;

    public void GainXP(Slime slime){
        currentXp+= (slime.XPGain* finalStats[Stats.Statstype.XPGain]);
        if (currentXp >= maxXp){
            LevelUp();
            level++;
            currentXp -= maxXp;
            maxXp *= 1.26f;
        }

        xpText.text = Convert.ToInt32(currentXp) + "/" + Convert.ToInt32(maxXp);
    }

    public void LevelUp(){
        UpgradeManager.um.UpgradeObtain(Convert.ToInt16(finalStats[Stats.Statstype.LevelUpgradeSlots]) );
        OnLevelUp?.Invoke(this, UpgradeManager.um);
    }
    
    //Health & Damage
    public void TakeDamage(float dmg){
        CalculateStats();
        dmg *=  (finalStats[Stats.Statstype.Resistance]);
        Health -= dmg;
        UpdateHPUI();
        if (Health <= 0){
            Die();
        }
        else{
            //on hit effects
            //spawn explosion for slimes
            float radius = 6;
            float force = 1000;
            Vector2 explosionPos = transform.position;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius, Enemies.value);
            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D srb = hit.GetComponent<Rigidbody2D>();

                if (srb != null){
                    Vector2 rot = srb.position - (Vector2)transform.position;
                    float distance = rot.magnitude;
                    srb.AddForce(rot.normalized* (force *  1/(distance+2f)));
                    //Utils.Utils.AddExplosionForce(srb, force, explosionPos, radius);
                    hit.gameObject.GetComponent<Slime>().tElapsed = -0.1f;
                }
            }
            
            //knockback player
        }
    }

    public void UpdateHPUI(){
        maxHealth = finalStats[Stats.Statstype.MaxHealth];
        hpBar.fillAmount = Health / finalStats[Stats.Statstype.MaxHealth];
        hpText.text = Math.Round(Health,1) + "/" + Convert.ToInt32(finalStats[Stats.Statstype.MaxHealth]);
    }

    public void Die(){
        Destroy(gameObject);
    }

    public void Heal(float amt){
        if (amt < 0){
            TakeDamage(-amt);
            return;
        }
        CalculateStats();
        Debug.Log("healed for " + amt);
        Health += amt * finalStats[Stats.Statstype.HPMultiplier];
        if (Health > finalStats[Stats.Statstype.MaxHealth]){
            Health = finalStats[Stats.Statstype.MaxHealth];
        }
        UpdateHPUI();
    }



    public void ApplyStatusEffect(StatusEffect statusEffect){
        debuffs.AddDebuff(statusEffect);
    }
    
    //Gold & etc.

    [Header("Gold")]
    public float goldAmount;
    public TMP_Text goldTxt;

    public void AddGold(float amt){
        goldAmount += amt * finalStats[Stats.Statstype.GoldMultiplier];
        UpdateGoldTxt();
    }

    public bool SpendGold(float amt){
        if(goldAmount>=amt){
            RemoveGold(amt);
            return true;
        }
        else{
            return false;
        }
    }

    public void RemoveGold(float amt){
        goldAmount -= amt;
        if (goldAmount < 0){
            goldAmount = 0;
        }
        UpdateGoldTxt();
    }

    public void UpdateGoldTxt(){
        goldTxt.text = String.Format(Convert.ToInt32(goldAmount).ToString(), $"{1234:n0}");
    }
}
