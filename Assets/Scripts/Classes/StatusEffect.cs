using System;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class StatusEffect{
    public Debuffs.DebuffTypes type;
    public GameObject particlePrefab; //actual prefab
    [HideInInspector]public GameObject particles; //reference
    public float duration;
    public float strength =1;
    public bool isStats;
    public Stats stats;
    public float maxTime = 20;
    public float hpPerSec = 0;
    public bool addStrength;

    public StatusEffect(float dur, float str){
        duration = dur;
        strength = str;
    }
    public StatusEffect(StatusEffect other)
    {
        particlePrefab = other.particlePrefab;
        duration = other.duration;
        strength = other.strength;
        type = other.type;
        isStats = other.isStats;
        stats = other.stats; //don't clone so that we can compare
        maxTime = other.maxTime;
        hpPerSec = other.hpPerSec;
        addStrength = other.addStrength;
    }

    private float tickElapsed;
    public virtual void Tick(Action<float> loseHP,Action<float> gainHP){
        //reducing duration is done in debuffs
        if (duration > maxTime){
            duration = maxTime;
        }

        if (hpPerSec != 0){
            tickElapsed += Time.deltaTime;
            if (tickElapsed >= 1){
                tickElapsed = 0;

                if (hpPerSec > 0){
                    gainHP(hpPerSec*strength);
                }
                else{
                    loseHP(-hpPerSec *strength);
                }

            }
        }
    }

    public virtual void Init(Transform parent){
        particles = GameObject.Instantiate(particlePrefab, parent);
    }

    public virtual void Remove(){
        GameObject.Destroy(particles);
    }
    
}