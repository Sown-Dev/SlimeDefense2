using System;
using UnityEngine;

public class CampfireObject: SpawnItemScript{
    public float startTime;
    public float range;
    [HideInInspector] public float time;
    public StatusEffect se;
    public GameObject circle;

    private void Start(){
        circle.transform.localScale *= range * 2;
        time = startTime;
    }
    
    public void Update(){
        time-=Time.deltaTime;
        if (Vector2.Distance(transform.position, p.transform.position) < range){
            p.ApplyStatusEffect(new StatusEffect(se));
        }
            
        if (time < 0){
            Destroy(gameObject);
        }
    }
}