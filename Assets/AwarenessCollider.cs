using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwarenessCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col){
        Slime s = col.gameObject.GetComponent<Slime>();
        if (s == null){
            return;
        }
        if(s.State == Slime.SlimeState.Aggressive)
            return;
        //if player enters trigger, become aggressive and set target
        else{
            s.State = Slime.SlimeState.Aggressive;
        }
    }
}
