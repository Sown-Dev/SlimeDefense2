using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProximityCheck : MonoBehaviour{
    public Slime mySlime;
    /*private void OnTriggerEnter2D(Collider2D col){
        if(mySlime.State == Slime.SlimeState.Aggressive)
            return;
        //if player enters trigger, become aggressive and set target
        if (col.gameObject.CompareTag("Player")){
            mySlime.State = Slime.SlimeState.Aggressive;
            mySlime.target = col.gameObject.transform;
        }
    }*/
}
