using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public delegate void Deleg(Slime s);

public class Slime : MonoBehaviour, IEnemyDamagable, IStatusEffectable{
    public static event Deleg OnDeath;

    // Start is called before the first frame update
    public Transform target;
    private Rigidbody2D rb;
    public GameObject deathParticles;
    public GameObject spawnOnDeath;
    public Animator am;
    public SpriteRenderer mySr;
    public SpriteRenderer hitSr;
    
    public SlimeState State;
    

    [field: SerializeField] public float maxHealth{ get; set; }
    public float Health{ get; set; }
    public float XPGain = 10f;
    public int creditCost = 10;
    public float speed = 200;
    public float goldDrop = 10;
    public Color color = Color.green;
    public float damage = 10;

    public StatusEffect onHitApply;

    [HideInInspector] public long id;

    private void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start(){
        debuffs = new Debuffs(transform);
        tElapsed = Random.Range(0, DELAY);
        rb = gameObject.GetComponent<Rigidbody2D>();
        Health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        am.SetTrigger("Spawn");
        hitSr.sprite = mySr.sprite;
    }

    public float DELAY = 0.9f;
    public float tElapsed = 0;

    private void Update(){
        debuffs.Tick(this, this);
        switch (State){
            //both states have the recycling code but passive slimes recycle at a lower threshold
            case SlimeState.Passive:{
                //recycling code
                if (target){
                    if (Vector2.Distance(transform.position, target.position) > 240 &&
                        GameManager.gm.currentSlimes > 100){
                        GameManager.gm.recycle(this);
                    }
                }

                break;
            }

            case SlimeState.Aggressive:{
                //recycling code
                if (target){
                    if (Vector2.Distance(transform.position, target.position) > 220 &&
                        GameManager.gm.currentSlimes > 120){
                        GameManager.gm.recycle(this);
                    }

                    if (Vector2.Distance(transform.position, target.position) > 20 &&
                        GameManager.gm.currentSlimes > 120 &&
                        id + 100 < GameManager.nextId){ //get old slimes based on id
                        GameManager.gm.recycle(this);
                    }
                }

                tElapsed += Time.deltaTime;

                // #TODO: add strengt multiplier
                float finalDelay = DELAY;
                if (debuffs[Debuffs.DebuffTypes.Slow]){
                    finalDelay *= 2;
                }

                if (tElapsed > DELAY){
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

        float finalSpeed = speed;
        if(debuffs[Debuffs.DebuffTypes.Slow])
            finalSpeed*=0.5f;
        rb.AddForce((dist.normalized + (Random.insideUnitCircle * 0.4f)) * finalSpeed);
        am.SetTrigger("Jump");
    }

    public void TakeDamage(float dmg){
        if (State != SlimeState.Aggressive)
            State = SlimeState.Aggressive;

        am.SetTrigger("Hit");
        Health -= dmg;
        if (Health <= 0){
            die();
        }
    }

    public void Heal(float amt){
        Health += amt;
        if (Health > maxHealth){
            Health = maxHealth;
        }
    }


    public void die(){
        Destroy(gameObject);

        GameObject particles = Instantiate(deathParticles, transform.position, transform.rotation);
        var m = particles.GetComponent<ParticleSystem>().main;
        m.startColor = color;
        if (spawnOnDeath != null)
            Instantiate(spawnOnDeath, transform.position, transform.rotation);

        GoldManager.gm.SpawnGold(transform.position, goldDrop);
        OnDeath?.Invoke(this);
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.GetComponent<IFriendlyDamagable>() != null){
            col.gameObject.GetComponent<IFriendlyDamagable>().TakeDamage(damage);
            col.rigidbody?.AddForce((col.gameObject.transform.position - transform.position).normalized *
                                    Player.p.finalStats[Stats.Statstype.KnockbackRecieved] * 400);
            rb.AddForce((col.gameObject.transform.position - transform.position).normalized * -150f);

            if (col.gameObject.GetComponent<IStatusEffectable>() != null && onHitApply != null){
                if(onHitApply.particles != null)
                    col.gameObject.GetComponent<IStatusEffectable>().ApplyStatusEffect(onHitApply);
            }
        }
    }

    public enum SlimeState{
        Passive = 1,
        Aggressive = 2,
    }


    //Debuff stuff
    public Debuffs debuffs{ get; set; }

    public void ApplyStatusEffect(StatusEffect statusEffect){
        debuffs.AddDebuff(statusEffect);
    }
}