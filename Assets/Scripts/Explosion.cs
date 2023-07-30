using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour{
    public float damage;
    public float radius;
    public float force;
    public LayerMask enemies;
    public List<Transform> ParticleSystems;

    private void Awake(){
        float scale = radius;
        foreach (Transform t in ParticleSystems){
            t.localScale = new Vector3(scale, scale,1);
        }
    }

    private void Start(){
        Explode();
        Destroy(gameObject, 1f);

    }

    public void Explode(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius,enemies.value);
        foreach (Collider2D nearbyObject in colliders){
            if (nearbyObject.isTrigger){
                //do nothing
            }
            else{
                if (nearbyObject.GetComponent<IEnemyDamagable>() != null){
                    nearbyObject.GetComponent<IEnemyDamagable>().TakeDamage(damage);
                }

                if (nearbyObject.GetComponent<Rigidbody2D>() != null){
                    nearbyObject.GetComponent<Rigidbody2D>()
                        .AddForce((nearbyObject.transform.position - transform.position).normalized * force);
                }
            }
        }
    }
    
}
