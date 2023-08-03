using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TeslaCoilItem : SpawnItemScript{
    private float cooldown = 1f;
    private float radius =3.5f;

    public LayerMask enemies;

    public LineRenderer lr;

    public CircleRenderer cr;
    
    private void Awake(){
        lr.enabled = false;
    }


    private float toW = 0;
    private float tElapsed;

    private void Start(){
        cr.xradius = radius;
        cr.yradius = radius;
        cr.CreatePoints();

    }

    private void Update(){
        lr.SetPosition(0, transform.position);
        lr.widthMultiplier = Mathf.Lerp(lr.widthMultiplier, toW, 11 * Time.deltaTime);
        

        tElapsed += Time.deltaTime;
        if ( tElapsed > cooldown){
            tElapsed = 0;
            Zap();
        }
    }

    public Animator am;

    void Zap(){
        am.SetTrigger("Shock");
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(transform.position, radius, enemies.value);

        float lowestDist = 1000;
        GameObject lowest = null;
        for (int i = 0; i < colliders.Length; i++){
            
            if (colliders[i].gameObject != gameObject){
                if (Vector2.Distance(transform.position, colliders[i].transform.position) < lowestDist){
                    lowestDist = Vector2.Distance(transform.position, colliders[i].transform.position);
                    lowest = colliders[i].gameObject;
                }
                
            }
        }
        if (lowest != null){
            lowest.GetComponent<IDamagable>().TakeDamage(p.finalStats[Stats.Statstype.Damage]*2);
            StartCoroutine(ShockFX(lowest.transform.position));
        }
    }

    IEnumerator ShockFX(Vector3 pos){
        lr.enabled = true;
        lr.SetPosition(1, pos);
        lr.widthMultiplier = 0.17f;
        toW = 0;
        yield return new WaitForSeconds(0.2f);
        lr.enabled = false;
    }
}