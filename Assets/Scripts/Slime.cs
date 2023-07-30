using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public delegate void Deleg(Slime s); 

public class Slime : MonoBehaviour, IEnemyDamagable, IStatusEffectable
{
    public static event Deleg OnDeath;
    // Start is called before the first frame update
    public Transform target;
    private Rigidbody2D rb;
    public GameObject deathParticles;
    public GameObject spawnOnDeath;
    public Animator am;
    public SlimeState State;
    
    [Header("Stats")]
    public float maxHealth;
    public float health;
    public float XPGain = 10f;
    public int creditCost = 10;
    public float speed = 200;

    public StatusEffect onHitApply;

    [HideInInspector] public long id;

    void Start(){
        debuffs = new Debuffs(transform);
        tElapsed =Random.Range(0,DELAY);
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        am.SetTrigger("Spawn");
    }

    public float DELAY = 0.9f;
    public float tElapsed = 0;
    private void Update(){
        debuffs.Tick(TakeDamage,Heal);
        switch (State){
            //both states have the recycling code but passive slimes recycle at a lower threshold
            case SlimeState.Passive:{
                //recycling code
                if (target){
                    if (Vector2.Distance(transform.position, target.position) > 240 && GameManager.gm.currentSlimes>80){
                        GameManager.gm.recycle(this);
                    }
                }
                break;
            }
                
            case SlimeState.Aggressive:{
                //recycling code
                if (target){
                    if (Vector2.Distance(transform.position, target.position) > 220 && GameManager.gm.currentSlimes>90){
                        GameManager.gm.recycle(this);
                    }
                    if (Vector2.Distance(transform.position, target.position) > 20 && GameManager.gm.currentSlimes>90 && 
                        id+100 < GameManager.nextId){ //get old slimes based on id
                        GameManager.gm.recycle(this);
                    }
                }
                tElapsed+= Time.deltaTime;
                
                // #TODO: add strengt multiplier
                float finalDelay = DELAY;
                if (debuffs[Debuffs.DebuffTypes.Slow])
                {
                    finalDelay *= 2;
                }
                
                if (tElapsed > DELAY ){
                    tElapsed = 0;
                    if (target){
                        Jump();
                    }
                }
                
                break;
            }
               
            
        }
        
        
       
        
    }

    public void Jump(){
        Vector2 dist = target.position - transform.position;
        rb.AddForce((dist.normalized+(Random.insideUnitCircle*0.4f)) *speed);
        am.SetTrigger("Jump");
    }
    
    public void TakeDamage(float dmg){
        if(State != SlimeState.Aggressive)
            State = SlimeState.Aggressive;
        
        am.SetTrigger("Hit");
        health -= dmg;
        if (health <= 0){
            die();
        }
    }

    public void Heal(float amt){
        health += amt;
        if (health > maxHealth){
            health = maxHealth;
        }
    }

   
    public void die(){
        Instantiate(deathParticles,transform.position,transform.rotation);
        Destroy(gameObject);
        OnDeath?.Invoke(this);
        if(spawnOnDeath!=null)
            Instantiate(spawnOnDeath,transform.position,transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.GetComponent<IFriendlyDamagable>() != null){
            col.gameObject.GetComponent<IFriendlyDamagable>().TakeDamage(10);
            col.rigidbody?.AddForce((col.gameObject.transform.position-transform.position).normalized*
                                    Player.p.finalStats[Stats.Statstype.KnockbackRecieved]*400);
            rb.AddForce((col.gameObject.transform.position-transform.position).normalized* -150f);

            if (col.gameObject.GetComponent<IStatusEffectabble>())
            {
                col.gameObject.GetComponent<IStatusEffectabble>().ApplyDebuff(OnHitApply);
            }
        }
    }

    public enum SlimeState{
        Passive=1,
        Aggressive=2,
    }

    
    //Debuff stuff
    [HideInInspector] public Debuffs debuffs;

    public void ApplyStatusEffect(StatusEffect statusEffect){
        debuffs.AddDebuff(statusEffect);
    }
}
